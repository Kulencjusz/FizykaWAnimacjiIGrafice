using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<HealthManager>())
        {
            ActionStateManager asm = other.gameObject.GetComponent<ActionStateManager>();
            if (asm != null && asm.currentWeapon != null && asm.currentWeapon.ammo != null)
            {
                asm.currentWeapon.ammo.extraAmmo += Random.Range(1, 20);
            }
            Destroy(gameObject);
        }
    }

}
