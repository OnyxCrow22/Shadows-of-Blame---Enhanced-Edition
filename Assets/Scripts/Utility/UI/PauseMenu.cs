using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    public PlayerControls pControls;
    public GameObject MainUI;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject audioSettings;
    public GameObject instructionsPanel;
    public GameObject graphicsPanel;
    public bool paused = false;

    void Awake()
    {
        pControls = new PlayerControls();   
    }

    void OnEnable()
    {
        pControls.Enable();
    }

    void OnDisable()
    {
        pControls.Disable();
    }

    private void Update()
    {
        if (pControls.Player.Pause.IsPressed())
        {
            if (!paused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        MainUI.SetActive(false);
        pauseMenu.SetActive(true);
        pControls.Player.Disable();
        pControls.UI.Enable();
        paused = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        MainUI.SetActive(true);
        pControls.Player.Enable();
        pControls.UI.Disable();
        paused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void Settings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Audio()
    {
        settingsMenu.SetActive(false);
        audioSettings.SetActive(true);
    }

    public void Instructions()
    {
        pauseMenu.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    public void Graphics()
    {
        settingsMenu.SetActive(false);
        graphicsPanel.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("FrontEnd");
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
