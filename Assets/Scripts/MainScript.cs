using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainScript : MonoBehaviour
{
    public GameObject optionsMenu;
    public TMP_Text txtTry;

    private int currentTry;

    private void OnDisable()
    {
        PlayerPrefs.SetInt("tryMap1", currentTry);
        PlayerPrefs.Save();
    }

    private void Start()
    {
        currentTry = PlayerPrefs.GetInt("tryMap1", 0);
        if (txtTry != null)
        {
            txtTry.text = currentTry.ToString();
        }
    }

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

    public void AddTry()
    {
        currentTry++;
        txtTry.text = currentTry.ToString();
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
