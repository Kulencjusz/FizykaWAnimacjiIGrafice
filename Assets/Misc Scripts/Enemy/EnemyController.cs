using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] private float timeToDeath = 5f;
    public bool isDead;
    Actions actions;
    RagdollManager ragdoll;
    private NavMeshAgent agent;
    LootDrop loot;

    public Image healthImage;

    private void Start()
    {
        loot = GetComponent<LootDrop>();
        actions = GetComponent<Actions>();
        agent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<RagdollManager>();
    }

    public void TakeDamage(float damage)
    {
        if(health > 0)
        {
            health -= damage;
            healthImage.fillAmount = health / 100f;
            if (health <= 0)
            {
                isDead = true;
                EnemyDeath();
                loot.DropBattery();
            }
        }
    }

    void EnemyDeath()
    {
        agent.speed = 0;
        if(actions != null)
        {
            actions.Death();
        }
        else if(ragdoll != null)
        {
            ragdoll.TriggerRagdoll();
        }
        Destroy(gameObject, timeToDeath);
    }
}
