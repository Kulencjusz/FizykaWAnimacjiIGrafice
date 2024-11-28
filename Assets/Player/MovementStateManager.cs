using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementStateManager : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float hzInput, vrInput;
    [HideInInspector] public Vector3 dir;
    CharacterController controller;
    public float groundYOffset;
    public LayerMask groundMask;
    Vector3 spherePosition;
    public float gravity = -9.81f;
    Vector3 velocity;
    PlayerControls playerControls;
    [SerializeField] float jumpForce = 10;
    [HideInInspector] public bool jumped, rolled;
    public float airSpeed = 1.5f;

    public MovementBaseState currentState;
    public MovementBaseState previousState;
    public IdleState Idle = new IdleState();
    public JumpState Jump = new JumpState();
    public WalkState Walk = new WalkState();
    public RunState Run = new RunState();
    public CrounchState Crounch = new CrounchState();
    public RollState Roll = new RollState();

    AudioSource audioSource;
    [HideInInspector] public Animator anim;
    [SerializeField] public AudioClip walkClip, runClip;
    public bool isRunning = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerControls = new PlayerControls();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        playerControls.PlayerMove.Move.started += _ => GetDirectionAndMove();
        SwitchState(Idle);
    }

    void Update()
    {
        if (!UIManager.IsGamePaused && !HealthManager.isDead)
        {
            GetDirectionAndMove();
            Gravity();
            Falling();
            currentState.UpdateState(this);
        }

    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }


    void GetDirectionAndMove()
    {
        Vector2 movement = playerControls.PlayerMove.Move.ReadValue<Vector2>();
        hzInput = movement.x;
        vrInput = movement.y;

        if(hzInput != 0 || vrInput != 0) 
        {
            if (isRunning)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(runClip);
                }
            }
            if (!audioSource.isPlaying && !isRunning)
            {
                audioSource.PlayOneShot(walkClip);
            }
        }

        Vector3 airDir = Vector3.zero;
        if (!IsGrounded())
        {
            airDir = transform.forward * vrInput + transform.right * hzInput;
        }
        else
        {

            dir = transform.forward * vrInput + transform.right * hzInput;
        }

        anim.SetFloat("hzInput", hzInput);
        anim.SetFloat("vrInput", vrInput);
        controller.Move((dir.normalized*moveSpeed + airDir.normalized * airSpeed)*Time.deltaTime);
    }

    public bool IsGrounded()
    {
        spherePosition = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if(Physics.CheckSphere(spherePosition, controller.radius - 0.05f, groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Gravity()
    {
        if(!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if(velocity.y < 0)
        {
            velocity.y = -2;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    void Falling()
    {
        anim.SetBool("isFalling", !IsGrounded());
    }

    public void JumpForce()
    {
        velocity.y += jumpForce;
    }

    public void Jumped()
    {
        jumped = true;
    }

    public void Rolled()
    {
        rolled = true;
    }
}
