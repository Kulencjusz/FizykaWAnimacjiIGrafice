using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionAccomplish : MonoBehaviour
{
    [SerializeField] UIManager manager;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip winClip;
    bool played = false;
    void Update()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        if (enemies.Length == 0)
        {
            if(!HealthManager.isDead)
            {
                manager.ShowWinScreen();
                if (!audioSource.isPlaying && !played)
                {
                    played = true;
                    audioSource.PlayOneShot(winClip);
                }
            }
        }
    }
}
