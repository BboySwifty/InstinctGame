using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public LevelLoader levelLoader;

    public void Resume()
    {
        Unpause();
        GameIsPaused = false;
    }

    public void Pause()
    {
        AudioListener.volume = 0f;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart()
    {
        Unpause();
        levelLoader.OpenScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Unpause();
        levelLoader.OpenScene(0);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void Unpause()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        AudioListener.volume = 1f;
        pauseMenuUI.SetActive(false);
    }
}
