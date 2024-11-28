using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    [SerializeField] ParticleSystem explosion;
    [HideInInspector] public WeaponManager weapon;
    public Vector3 dir;

    Rigidbody rig;
    public GameObject bulletHole;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        rig.velocity = transform.forward * 50f;
        Destroy(gameObject, timeToDestroy);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(bulletHole, transform.position,
                Quaternion.LookRotation(new Vector3(0,-90,0)));
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Instantiate(bulletHole, transform.position,
                Quaternion.LookRotation(new Vector3(90, 0, 0)));
        }
        if (collision.gameObject.CompareTag("Exp"))
        {
            Instantiate(explosion, collision.transform.position, Quaternion.identity);
            Explosion exps = collision.gameObject.GetComponentInChildren<Explosion>();
            exps.Activate();
            Destroy(collision.gameObject, 0.5f);
        }
        if (collision.gameObject.GetComponent<Destructable>())
        {
            Destructable destructable = collision.gameObject.GetComponent<Destructable>();
            if (destructable != null)
            {
                destructable.Destruct();
            }
        }
        if (collision.gameObject.GetComponent<EnemyController>())
        {
            EnemyController enemyHealth = collision.gameObject.GetComponent<EnemyController>();
            enemyHealth.TakeDamage(weapon.damage);

            if(enemyHealth.health <= 0 && !enemyHealth.isDead)
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                if(rb != null)
                {
                    rb.AddForce(dir * weapon.enemyKickBackForce, ForceMode.Impulse);
                }
                enemyHealth.isDead = false;
            }
        }
        if (collision.gameObject.GetComponent<HealthManager>())
        {
            HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
            healthManager.TakeDamage(EnemyNavigation.damage);
            Debug.Log("Hit player");
        }
        Destroy(this.gameObject);
    }
}
