using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    public GameObject battery;

    public void DropBattery()
    {
        int roll = Random.Range(0, 100);
        if(roll >= 10)
        {
            Vector3 position = transform.position;
            Instantiate(battery, new Vector3(position.x, 0.1f, position.z), Quaternion.identity);
        }
    }
}
