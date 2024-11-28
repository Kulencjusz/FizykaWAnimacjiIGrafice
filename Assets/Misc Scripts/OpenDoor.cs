using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("nearb");
        anim.SetBool("character_nearby", true);
    }

    private void OnCollisionExit(Collision collision)
    {
        anim.SetBool("character_nearby", false);
    }
}
