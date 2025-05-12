using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelsMenu : MonoBehaviour
{

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadLevelCoroutine(levelName));
    }

    private IEnumerator LoadLevelCoroutine(string levelName)
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(levelName);
    }

    public void resetTries(int max)
    {
        for (int i = 0; i < max; i++)
        {
            PlayerPrefs.SetInt($"tryMap{i}", 0);
        }
        PlayerPrefs.Save();
    }
}
