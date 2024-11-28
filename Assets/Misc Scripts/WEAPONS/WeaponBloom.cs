using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBloom : MonoBehaviour
{
    [SerializeField] float defaultBloomAngle = 3;
    [SerializeField] float walkBloomMultiplier = 1.5f;
    [SerializeField] float runBloomMultiplier = 2;
    [SerializeField] float crouchBloomMultiplier = 0.5f;
    [SerializeField] float adsBloomMultiplier = 0.5f;

    MovementStateManager movement;
    AimStateManager aim;

    float currentBloom;

    private void Start()
    {
        movement = GetComponentInParent<MovementStateManager>();
        aim = GetComponentInParent<AimStateManager>();
    }

    public Vector3 BloomAngle(Transform barrelPos)
    {
        if(movement.currentState == movement.Idle)
        {
            currentBloom = defaultBloomAngle;
        }
        else if(movement.currentState == movement.Walk)
        {
            currentBloom = defaultBloomAngle * walkBloomMultiplier;
        }
        else if (movement.currentState == movement.Run)
        {
            currentBloom = defaultBloomAngle * runBloomMultiplier;
        }
        else if (movement.currentState == movement.Crounch)
        {
            if(movement.dir.magnitude == 0)
            {
                currentBloom = defaultBloomAngle * crouchBloomMultiplier;
            }
            else
            {
                currentBloom = defaultBloomAngle * crouchBloomMultiplier * walkBloomMultiplier;

            }
        }

        if(aim.currentState == aim.Aim)
        {
            currentBloom *= adsBloomMultiplier;
        }
        float randomX = Random.Range(-currentBloom, currentBloom);
        float randomY = Random.Range(-currentBloom, currentBloom);
        float randomZ = Random.Range(-currentBloom, currentBloom);
        Vector3 randomRotation = new Vector3(randomX, randomY, randomZ);
        return barrelPos.localEulerAngles + randomRotation;
    }
}
