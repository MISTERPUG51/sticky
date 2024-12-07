using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Globalization;
using UnityEngine.UI;
using System;
using UnityEditor;
using System.IO;



public class menubuttoncontrol : MonoBehaviour
{
    public TMP_Dropdown levelSelectDropdown;
    public GameObject levelSelectWarning;
    public GameObject MainMenuUI;
    public GameObject LevelSelectUI;
    public GameObject SaveDataMenuUI;
    public GameObject OldSaveNotification;
    public Image LevelPreviewImage;
    public Sprite level_icon_0;
    public Sprite level_icon_1;
    public TMP_Text LevelNameText;
    public Sprite level_icon_locked;
    public AudioSource BuzzAudioSource;

    public TMP_Text SaveFileCreationTimeText;
    public TMP_Text UnlockedLevelsText;
    public TMP_Dropdown LevelTimeDropdown;
    public TMP_Text LevelTimeText;

    public GameObject DeleteDataMenuUI;


    public GameObject CustomLevelsMenuUI;


    //These sprites are the level preview images on the level select screen.
    public Sprite level_icon_2;
    public Sprite level_icon_3;
    public Sprite level_icon_4;
    public Sprite level_icon_5;
    public Sprite level_icon_6;
    public Sprite level_icon_7;
    public Sprite level_icon_8;
    public Sprite level_icon_9;
    public Sprite level_icon_10;

    public SaveHandler SaveHandler;

    public void Start()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/updater.exe"))
        {
            Debug.Log("Deleting temporary updater file.");
            System.IO.File.Delete(Application.persistentDataPath + "/updater.exe");
        }
        Debug.Log(Application.persistentDataPath);
        Debug.Log(Application.persistentDataPath + "/save.json");

        //Creates a save file if it does not already exist.
        if (!System.IO.File.Exists(Application.persistentDataPath + "/save.json"))
        {
            SaveHandler.SaveDataVersion = 0;
            SaveHandler.SaveData();
        }
        SaveHandler.UpdateSaveData();
        SaveHandler.LoadData();
        Debug.Log(System.DateTime.Now);
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


    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    
    

    public void MainMenuInSampleScene()
    {
        SaveDataMenuUI.SetActive(false);
        LevelSelectUI.SetActive(false);
        CustomLevelsMenuUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void LevelSelectionChanged()
    {
        //If the selected level is locked, show the padlock level icon.
        //If the selected level is unlocked, show the corresponding icon.
        if (levelSelectDropdown.value > SaveHandler.UnlockedLevels)
        {
            LevelPreviewImage.sprite = level_icon_locked;
            LevelNameText.text = "Locked";
        } else {
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
                LevelNameText.text = "Level 3: Spiral";
            }
            else if (levelSelectDropdown.value == 4)
            {
                LevelPreviewImage.sprite = level_icon_4;
                LevelNameText.text = "Level 4: Fiery Fun";
            }
            else if (levelSelectDropdown.value == 5)
            {
                LevelPreviewImage.sprite = level_icon_5;
                LevelNameText.text = "Level 5: More fire = more fun";
            }
            else if (levelSelectDropdown.value == 6)
            {
                LevelPreviewImage.sprite = level_icon_6;
                LevelNameText.text = "Level 6: rock";
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
        //If the selected level is unlocked, load the scene for that level.
        //If the selected level is locked, play a buzz sound.
        if (levelSelectDropdown.value != 0)
        {
            if (levelSelectDropdown.value <= SaveHandler.UnlockedLevels)
            {
                SceneManager.LoadScene("Level" + levelSelectDropdown.value);
            } else
            {
                BuzzAudioSource.Play();
            }
        }
    }

    public void OldSaveOKButton()
    {
        PlayerPrefs.DeleteKey("UnlockedLevels");
        PlayerPrefs.SetInt("SaveDataVersion", PlayerPrefs.GetInt("CurrentSaveDataVersion"));
        PlayerPrefs.Save();
        OldSaveNotification.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void FeedBackButton()
    {
        Application.OpenURL("https://github.com/MISTERPUG51/sticky/issues/new");
    }

    public void SaveDataMenu()
    {
        DeleteDataMenuUI.SetActive(false);
        MainMenuUI.SetActive(false);
        SaveDataMenuUI.SetActive(true);
        SaveFileCreationTimeText.text = "Save file created: " + SaveHandler.CreatedDate;
        UnlockedLevelsText.text = "Unlocked levels: " + SaveHandler.UnlockedLevels;
    }

    public void DeleteDataMenu()
    {
        SaveDataMenuUI.SetActive(false);
        DeleteDataMenuUI.SetActive(true);
    }

    public void DeleteData()
    {
        System.IO.File.Delete(Application.persistentDataPath + "/save.json");
        SceneManager.LoadScene("SampleScene");
    }

    public void LevelTimeDropdownChanged()
    {
        if (LevelTimeDropdown.value == 0)
        {
            LevelTimeText.text = "Select a level to see your best time.";
        }
        if (LevelTimeDropdown.value == 1)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(SaveHandler.Level1Time);
            string GameStopWatchTimeFormattedToText = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
            LevelTimeText.text = GameStopWatchTimeFormattedToText;
        }
        if (LevelTimeDropdown.value == 2)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(SaveHandler.Level2Time);
            string GameStopWatchTimeFormattedToText = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
            LevelTimeText.text = GameStopWatchTimeFormattedToText;
        }
        if (LevelTimeDropdown.value == 3)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(SaveHandler.Level3Time);
            string GameStopWatchTimeFormattedToText = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
            LevelTimeText.text = GameStopWatchTimeFormattedToText;
        }
        if (LevelTimeDropdown.value == 4)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(SaveHandler.Level4Time);
            string GameStopWatchTimeFormattedToText = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
            LevelTimeText.text = GameStopWatchTimeFormattedToText;
        }
        if (LevelTimeDropdown.value == 5)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(SaveHandler.Level5Time);
            string GameStopWatchTimeFormattedToText = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
            LevelTimeText.text = GameStopWatchTimeFormattedToText;
        }
        if (LevelTimeDropdown.value == 6)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(SaveHandler.Level6Time);
            string GameStopWatchTimeFormattedToText = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
            LevelTimeText.text = GameStopWatchTimeFormattedToText;
        }
        if (LevelTimeText.text == "00:00:00:000")
        {
            LevelTimeText.text = "Not set";
        }
    }





    public void CustomLevelsMenu()
    {
        MainMenuUI.SetActive(false);
        CustomLevelsMenuUI.SetActive(true);
    }

    public void LoadCustomLevel()
    {
        SceneManager.LoadScene("CustomLevel");
    }
}
