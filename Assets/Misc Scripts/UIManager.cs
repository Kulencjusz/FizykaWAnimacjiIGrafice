using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text currentAmmo, ammo, enemyName;
    [SerializeField] public GameObject enemy;
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject crossHairHolder;
    [SerializeField] public Image crosshair;
    [SerializeField] public GameObject deathScreen, winScreen;
    [SerializeField] public GameObject mission;

    FadeUI fade;

    public static bool IsGamePaused = false;

    private void Awake()
    {
        fade = GetComponent<FadeUI>();
    }

    private void Update()
    {
        if (!HealthManager.isDead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (IsGamePaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                ShowMission();
            }
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                HideMission();
            }
        }
    }

    public void Resume()
    {
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void ShowDeathScreen()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        deathScreen.SetActive(true);
        fade.ShowUI();
    }

    public void ShowWinScreen()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        winScreen.SetActive(true);
    }

    public void EnableCrosshair()
    {
        crossHairHolder.SetActive(true);
    }

    public void DisableCrosshair()
    {
        crossHairHolder?.SetActive(false);
    }

    public void GoMenu()
    {
        IsGamePaused = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowEnemy(string name)
    {
        enemy.SetActive(true);
        enemyName.text = name;
    }

    public void Hide(GameObject go)
    {
        go.SetActive(false);
    }

    public void ChangeAmmo(int current, int whole)
    {
        currentAmmo.text = current.ToString();
        ammo.text = whole.ToString();
    }

    public void ChangeCrosshairActive()
    {
        crosshair.color = Color.blue;
    }

    public void ChangeCrosshairDeafult()
    {
        crosshair.color = new Color(0,190,255, 255);
    }

    public void ShowMission()
    {
        mission.SetActive(true);
    }

    public void HideMission()
    {
        mission.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
