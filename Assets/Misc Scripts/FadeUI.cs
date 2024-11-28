using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup group;
    public bool fadeIn = false, fadeOut = false;

    public void ShowUI()
    {
        fadeIn = true;
    }

    public void HideUI()
    {
        fadeOut = true;
    }

    private void Update()
    {
        if(!UIManager.IsGamePaused)
        {
            if(fadeIn)
            {
                if(group.alpha < 1)
                {
                    group.alpha += Time.deltaTime;
                }
                if(group.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }
    }
}
