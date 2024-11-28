using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    [SerializeField] private bool scrollLeft;
    private float singleTextureWidth;

    private void Start()
    {
        SetupTextures();
        if(scrollLeft)
        {
            scrollSpeed = -scrollSpeed;
        }
    }

    private void SetupTextures()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        singleTextureWidth = sprite.texture.width / sprite.pixelsPerUnit / 0.16f;
    }

    private void Scroll()
    {
        float delta = scrollSpeed * Time.deltaTime;
        transform.position += new Vector3(delta, 0f, 0f);
    }

    private void ChechReset()
    {
        if((Mathf.Abs(transform.position.x) - singleTextureWidth) > 0)
        {
            transform.position = new Vector3(0f, 0f, 0f);
        }
    }

    private void Update()
    {
        Scroll();
        ChechReset();
    }
}
