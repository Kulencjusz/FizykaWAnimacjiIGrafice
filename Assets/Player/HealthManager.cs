using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] Image playerHealth;
    public float health;

    public static bool isDead;
    [SerializeField] UIManager manager;
    [SerializeField] Animator anim;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip deathSound, hurtSound;


    private void Update()
    {
        if (!UIManager.IsGamePaused)
        {
            if (health <= 0 && !isDead)
            {
                isDead = true;
                anim.SetTrigger("Death");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        audioSource.PlayOneShot(hurtSound);
        health -=damage;
        playerHealth.fillAmount = health / 100f;
    }

    public void Heal(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, 100);
        playerHealth.fillAmount = health / 100f;
    }

    public void Death()
    {
        audioSource.PlayOneShot(deathSound);
        manager.ShowDeathScreen();
    }
}
