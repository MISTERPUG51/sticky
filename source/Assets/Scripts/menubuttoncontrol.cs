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
    public Slider MusicVolumeSlider;
    public GameObject MainMenuUI;
    public GameObject LevelSelectUI;
    public Image LevelPreviewImage;
    public Sprite level_icon_0;
    public Sprite level_icon_1;
    public TMP_Text LevelNameText;
    public Sprite level_icon_locked;
    public AudioSource BuzzAudioSource;
    public GameObject PauseMenuUI;
    public PlayerMovement PlayerMovement;
    public GameObject Controls;
    public Sprite level_icon_2;
    public Sprite level_icon_3;
    public Sprite level_icon_4;
    public Sprite level_icon_5;
    public Sprite level_icon_6;
    public Sprite level_icon_7;
    public Sprite level_icon_8;
    public Sprite level_icon_9;
    public Sprite level_icon_10;

    public void Awake()
    {
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
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
    public void LevelSelect()
    {
        MainMenuUI.SetActive(false);
        LevelSelectUI.SetActive(true);
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

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void ChangeMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", MusicVolumeSlider.value);
        PlayerPrefs.Save();
    }
    public void ResetSettings()
    {
        PlayerPrefs.DeleteKey("MusicVolume");
        SceneManager.LoadScene("Settings");
    }

    public void VisitWebsite()
    {
        Application.OpenURL("https://misterpug51.github.io/programs/sticky/");
    }

    public void MainMenuInSampleScene()
    {
        LevelSelectUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void LevelSelectionChanged()
    {
        if (levelSelectDropdown.value > PlayerPrefs.GetInt("UnlockedLevels",1))
        {
            LevelPreviewImage.sprite = level_icon_locked;
            LevelNameText.text = "Locked";
        } else
        {
            if (levelSelectDropdown.value == 0)
            {
                LevelPreviewImage.sprite = level_icon_0;
                LevelNameText.text = "No level selected.";
            }
            else if (levelSelectDropdown.value == 1)
            {
                LevelPreviewImage.sprite = level_icon_1;
                LevelNameText.text = "Level 1: The Beginning";
            }
            else if (levelSelectDropdown.value == 2)
            {
                LevelPreviewImage.sprite = level_icon_2;
                LevelNameText.text = "Level 2: Learning Curve";
            }
            else if (levelSelectDropdown.value == 3)
            {
                LevelPreviewImage.sprite = level_icon_3;
                LevelNameText.text = "Level 3: Back and Forth";
            }
            else if (levelSelectDropdown.value == 4)
            {
                LevelPreviewImage.sprite = level_icon_4;
                LevelNameText.text = "Level 4: I can't think of a good name";
            }
            else if (levelSelectDropdown.value == 5)
            {
                LevelPreviewImage.sprite = level_icon_5;
                LevelNameText.text = "Level 5: Is it getting hot in here?";
            }
            else if (levelSelectDropdown.value == 6)
            {
                LevelPreviewImage.sprite = level_icon_6;
                LevelNameText.text = "Level 6: More fire = more fun";
            }
            else if (levelSelectDropdown.value == 7)
            {
                LevelPreviewImage.sprite = level_icon_7;
                LevelNameText.text = "Level 7: Inferno";
            }
            else if (levelSelectDropdown.value == 8)
            {
                LevelPreviewImage.sprite = level_icon_8;
                LevelNameText.text = "Level 8: Spinny spin spin";
            }
            else if (levelSelectDropdown.value == 9)
            {
                LevelPreviewImage.sprite = level_icon_9;
                LevelNameText.text = "Level 9: The walls can move?!?";
            }
            else if (levelSelectDropdown.value == 10)
            {
                LevelPreviewImage.sprite = level_icon_10;
                LevelNameText.text = "Level 10: Nothing to see here ;)";
            }
        }
    }

    public void StartGame()
    {
        if (levelSelectDropdown.value != 0)
        {
            if (levelSelectDropdown.value <= PlayerPrefs.GetInt("UnlockedLevels", 1))
            {
                SceneManager.LoadScene("Level" + levelSelectDropdown.value);
            } else
            {
                BuzzAudioSource.Play();
            }
        }
    }

    public void PauseGame()
    {
        PauseMenuUI.SetActive(true);
    }
    public void UnpauseGame()
    {
        PauseMenuUI.SetActive(false);
        PlayerMovement.ResumeGame();
    }

    public void HideControls()
    {
        Controls.SetActive(false);
    }
}
