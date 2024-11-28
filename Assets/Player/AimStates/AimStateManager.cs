using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;

public class AimStateManager : MonoBehaviour
{
    [SerializeField] private Transform camFollowPos;
    [SerializeField] private float mouseSense = 1f;
    private float xAxis, yAxis;
    public AimBaseState currentState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();
    [HideInInspector] public Animator anim;

    [HideInInspector] public CinemachineVirtualCamera vCam;
    public float adsFov = 30f;
    public float sniperAdsFov = 80f;
    [HideInInspector] public float hipFov;
    [HideInInspector] public float currentFov;
    public float fovSmoothSpeed = 10f;

    [SerializeField] public Transform aimPos;
    public Vector3 actualAimPos;
    [SerializeField] float aimSmoothSpeed = 20f;
    [SerializeField] LayerMask aimMask;

    float xFollowPos;
    float yFollowPos, ogYPos;
    [SerializeField] float crouchCameraHeight = 0.6f;
    [SerializeField] float shoulderSwapSpeed = 10;
    MovementStateManager movement;

    MultiAimConstraint[] multiAims;
    WeightedTransform aimPosWeightedTranform;
    [SerializeField] public UIManager uiManager;

    [HideInInspector] public ActionStateManager actionManager;
    [SerializeField] GameObject sniperScope;

    private void Awake()
    {
        actionManager = GetComponent<ActionStateManager>();
        aimPos = new GameObject().transform;
        aimPos.name = "AimPosition";

        aimPosWeightedTranform.transform = aimPos;
        aimPosWeightedTranform.weight = 1;

        multiAims = GetComponentsInChildren<MultiAimConstraint>();
        foreach(MultiAimConstraint constraint in multiAims)
        {
            var data = constraint.data.sourceObjects;
            data.Clear();
            data.Add(aimPosWeightedTranform);
            constraint.data.sourceObjects = data;
        }
    }

    private void Start()
    {
        movement = GetComponent<MovementStateManager>();
        xFollowPos = camFollowPos.localPosition.x;
        ogYPos = camFollowPos.localPosition.y;
        yFollowPos = ogYPos;
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFov = vCam.m_Lens.FieldOfView;
        anim = GetComponent<Animator>();
        SwitchState(Hip);
    }

    private void Update()
    {
        if(!UIManager.IsGamePaused && !HealthManager.isDead)
        {
            xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
            yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
            yAxis = Mathf.Clamp(yAxis, -80f, 60f);
            vCam.m_Lens.FieldOfView = currentFov;
            Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray = Camera.main.ScreenPointToRay(screenCentre);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
            {
                if (hit.collider.GetComponentInParent<EnemyController>() != null)
                {
                    uiManager.ShowEnemy(hit.collider.name);
                    uiManager.ChangeCrosshairActive();
                }
                else
                {
                    uiManager.Hide(uiManager.enemy);
                    uiManager.ChangeCrosshairDeafult();
                }
                aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
                actualAimPos = hit.point;
            }
            MoveCamera();
            currentState.UpdateState(this);
        }
    }

    private void LateUpdate()
    {
        if (!UIManager.IsGamePaused)
        {
            camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
        }
    }

    public void OnSniperScope()
    {
        sniperScope.SetActive(true);
    }

    public void OffSniperScope()
    {
        sniperScope.SetActive(false);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void MoveCamera()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            xFollowPos = -xFollowPos;
        }
        if (movement.currentState == movement.Crounch)
        {
            yFollowPos = crouchCameraHeight;
        }
        else
        {
            yFollowPos = ogYPos;
        }
        Vector3 newFollowPos = new Vector3(xFollowPos, yFollowPos, camFollowPos.localPosition.z);
        camFollowPos.localPosition = Vector3.Lerp(camFollowPos.localPosition, newFollowPos, shoulderSwapSpeed * Time.deltaTime);
    }
}
