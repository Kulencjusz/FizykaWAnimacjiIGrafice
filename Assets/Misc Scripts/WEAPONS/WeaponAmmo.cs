using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponAmmo : MonoBehaviour
{
    public int clipSize;
    public int extraAmmo;
    public int currentAmmo;
    public AudioClip reloadSound;

    private void Start()
    {
        currentAmmo = clipSize;
    }

    public void Reload()
    {
        if(extraAmmo >= clipSize)
        {
            int ammoToReload = clipSize - currentAmmo;
            extraAmmo -= ammoToReload;
            currentAmmo += ammoToReload;
        }
        else if(extraAmmo > 0)
        {
            if(extraAmmo+currentAmmo > clipSize)
            {
                int leftoverAmmo = extraAmmo + currentAmmo - clipSize;
                extraAmmo = leftoverAmmo;
                currentAmmo = clipSize;
            }
            else
            {
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
    }
}
