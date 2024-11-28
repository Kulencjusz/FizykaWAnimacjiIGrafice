using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    public float attackRange = 5f, sightRange = 20f, timeBetweenAttack = 3f;
    public float fieldOfViewAngle = 90f;

    public Transform[] wayPoints;
    int waypointIndex;
    public Vector3 target;

    private Animator anim;
    private bool isAttacking;
    Actions actions;
    public static float damage = 15f;

    public Transform[] projectileSpawnPoints;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip shot;
    EnemyController enemyController;
    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
        agent = GetComponent<NavMeshAgent>();
        actions = GetComponent<Actions>();
        anim = GetComponent<Animator>();
        anim.SetFloat("Speed", agent.speed);
        UpdateDestination();
    }

    private void Update()
    {
        if (!UIManager.IsGamePaused)
        {
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            if (distanceToPlayer <= sightRange && !HealthManager.isDead)
            {
                Vector3 direction = player.position - transform.position;
                direction.y = 0;
                transform.rotation = Quaternion.LookRotation(direction);
                if (distanceToPlayer > attackRange)
                {
                    anim.SetFloat("Speed", agent.speed);
                    actions.Run();
                    isAttacking = false;
                    agent.isStopped = false;
                    StopAllCoroutines();
                    ChasePlayer();
                }
                else if (!isAttacking && !enemyController.isDead)
                {
                    anim.SetFloat("Speed", 0);
                    agent.isStopped = true;
                    StartCoroutine(AttackPlayer());
                }
            }
            else
            {
                Patrol();
            }

            if (HealthManager.isDead)
            {
                agent.isStopped = true;
            }
        }
    }

    void Patrol()
    {
        if (Vector3.Distance(transform.position, target) < 1)
        {
            IterateWayPointIndex();
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        target = wayPoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    void IterateWayPointIndex()
    {
        waypointIndex = Random.Range(0, wayPoints.Length);
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        yield return new WaitForSeconds(timeBetweenAttack);
        anim.SetBool("Aiming", true);
        actions.Aiming();
        foreach (Transform spawn in projectileSpawnPoints)
        {
            Instantiate(particleSystem, spawn.position, transform.rotation);
            Instantiate(projectilePrefab, spawn.position, transform.rotation);
            anim.SetBool("Aiming", false);
            anim.SetTrigger("Attack");
            actions.Attack();
            audioSource.PlayOneShot(shot);
        }
        isAttacking = false;
    }
}
