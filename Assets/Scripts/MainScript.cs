using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
            }
            else
            {
                optionsMenu.SetActive(true);
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
