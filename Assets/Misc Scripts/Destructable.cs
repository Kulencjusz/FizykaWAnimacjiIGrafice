using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Destructable : MonoBehaviour
{
    [SerializeField] GameObject destructedPrefab;
    LootDrop lootDrop;

    private void Awake()
    {
        lootDrop = GetComponent<LootDrop>();
    }

    public void Destruct()
    {
        Destroy(this.gameObject);
        Instantiate(destructedPrefab, transform.position, transform.rotation);
        lootDrop.DropBattery();
    }
}
