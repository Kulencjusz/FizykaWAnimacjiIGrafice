using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    bool canDamage = false;
    bool damaged = false;
    public void Activate()
    {
        canDamage = true;
        damaged = false;
    }

    public float detectionRadius = 5f; 
    public LayerMask detectionLayer, detectionLayerEnemy;

    private void Start()
    {
        detectionLayer = LayerMask.GetMask("Player");
        detectionLayerEnemy = LayerMask.GetMask("Default");
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);
        foreach (Collider collider in colliders)
        {
            if (canDamage && !damaged)
            {
                HealthManager healthManager = collider.gameObject.GetComponent<HealthManager>();
                if(healthManager != null)
                {
                    healthManager.TakeDamage(20f);
                }
                damaged = true;
            }
        }
        Collider[] colliders2 = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayerEnemy);
        foreach (Collider collider in colliders2)
        {
            if (canDamage && !damaged)
            {
                EnemyController enemyHealth = collider.gameObject.GetComponent<EnemyController>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(20f);
                }
                damaged = true;
            }
        }
    }
}
