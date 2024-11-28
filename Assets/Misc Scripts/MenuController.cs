using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScene, settingsScene, selectLevel;
    [SerializeField] private GameObject controlsScene, soundScene, effectScene;
    [SerializeField] private Toggle toggle;
    public static bool enableEffect = true;

    public void StartGame()
    {
        mainMenuScene.SetActive(false);
        selectLevel.SetActive(true);
        HealthManager.isDead = false;
        UIManager.IsGamePaused = false;
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void GoToSettings()
    {
        mainMenuScene.SetActive(false);
        controlsScene.SetActive(false);
        effectScene.SetActive(false);
        soundScene.SetActive(false);
        settingsScene.SetActive(true);
    }

    public void GoToMenu()
    {
        mainMenuScene.SetActive(true);
        settingsScene.SetActive(false);
        selectLevel.SetActive(false);
    }

    public void GoToControls()
    {
        settingsScene.SetActive(false);
        controlsScene.SetActive(true);
    }

    public void GoToSounds()
    {
        settingsScene.SetActive(false);
        soundScene.SetActive(true);
    }

    public void GoToEffects()
    {
        settingsScene.SetActive(false);
        effectScene.SetActive(true);
    }

    public void OnToggleChange()
    {
        enableEffect = toggle.isOn;
    }

    public void SelectLevel_Base()
    {
        SceneManager.LoadScene("Level_SpaceBase");
    }

    public void SelectLevel_SnowPlanet()
    {
        SceneManager.LoadScene("Level_SnowPlanet");
    }

    public void SelectLevel_Warehouse()
    {
        SceneManager.LoadScene("Level_Warehouse");
    }

    public void SelectLevel_Spaceship()
    {
        SceneManager.LoadScene("Level_SpaceReactor");
    }

}
