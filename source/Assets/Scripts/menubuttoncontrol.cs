using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Globalization;
using UnityEngine.UI;
using System;



public class menubuttoncontrol : MonoBehaviour
{
    public TMP_Dropdown levelSelectDropdown;
    public GameObject levelSelectWarning;
    public void Awake()
    {
        levelSelectWarning.SetActive(false);
        if (CultureInfo.InvariantCulture.CompareInfo.IndexOf(SystemInfo.processorType, "ARM", CompareOptions.IgnoreCase) >= 0)
        {
            if (Environment.Is64BitProcess)
                Debug.Log("ARM64");
            else
                Debug.Log("ARM");
        }
        else
        {
            // Must be in the x86 family.
            if (Environment.Is64BitProcess)
                Debug.Log("x64");
            else
                Debug.Log("x86");
        }
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

    public void CheckUpdate()
    {
        SceneManager.LoadScene("Update");
    }
}
