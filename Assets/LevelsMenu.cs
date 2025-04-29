using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour
{

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    //public Button[] buttons;
    //public GameObject levelButtons;

    //private void Awake()
    //{
    //    ButtonsToArray();
    //    int unleckedLevels = PlayerPrefs.GetInt("UnleckedLevel", 1);
    //    for (int i = 0; i < buttons.Length; i++)
    //    {
    //        if (i + 1 > unleckedLevels)
    //        {
    //            buttons[i].interactable = false;
    //        }
    //        else
    //        {
    //            buttons[i].interactable = true;
    //        }
    //    }
    //}

    //void ButtonsToArray()
    //{
    //    int childCount = levelButtons.transform.childCount;
    //    buttons = new Button[childCount];
    //    for (int i = 0; i < childCount; i++)
    //    {
    //        buttons[i] = levelButtons.transform.GetChild(i).GetComponent<Button>();
    //    }
    //}
}
