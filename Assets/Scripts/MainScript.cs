using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class MainScript : MonoBehaviour
{
    public GameObject optionsMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playPauseGame();
        }
    }

    public void playPauseGame()
    {
        if (optionsMenu.activeSelf)
        {
            optionsMenu.SetActive(false);
            Time.timeScale = 1f; // Resume the game
        }
        else
        {
            optionsMenu.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoHome()
    {
        StartCoroutine(GoHomeCoroutine());
    }

    private IEnumerator GoHomeCoroutine()
    {
        Time.timeScale = 1f; // Resume the game

        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("MainMenu");
    }
}
