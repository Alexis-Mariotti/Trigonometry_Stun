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
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoHome()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
