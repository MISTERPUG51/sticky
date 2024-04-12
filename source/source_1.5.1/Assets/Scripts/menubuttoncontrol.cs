using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class menubuttoncontrol : MonoBehaviour
{
    public TMP_Dropdown levelSelectDropdown;
    public GameObject levelSelectWarning;
    public void Awake()
    {
        levelSelectWarning.SetActive(false);
    }
    public void StartGame()
    {
        if (levelSelectDropdown.value == 0)
        {
            levelSelectWarning.SetActive(true);
        } else
        {
            SceneManager.LoadScene("Level" + levelSelectDropdown.value);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
