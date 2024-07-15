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
    public GameObject MainMenuUI;
    public GameObject LevelSelectUI;
    public GameObject OldSaveNotification;
    public Image LevelPreviewImage;
    public Sprite level_icon_0;
    public Sprite level_icon_1;
    public TMP_Text LevelNameText;
    public Sprite level_icon_locked;
    public AudioSource BuzzAudioSource;
    public GameObject PauseMenuUI;
    public PlayerMovement PlayerMovement;
    public GameObject Controls;

    public int UnlockedLevels;
    public int PlayerColor;
    public int SaveDataVersion;
    public float MusicVolume;


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



    [Serializable]
    public class JsonSaveDataClass
    {
        public int SaveDataVersion;
        public int PlayerColor;
        public int UnlockedLevels;
        public float MusicVolume;
    }
    public void Start()
    {
        Debug.Log(Application.persistentDataPath);
        Debug.Log(Application.persistentDataPath + "/save.json");
        //PlayerPrefs.SetInt("CurrentSaveDataVersion", 1);
        //PlayerPrefs.Save();
        //Debug.Log("SaveDataVersion=" + PlayerPrefs.GetInt("SaveDataVersion", 0));
        //Debug.Log("UnlockedLevels=" + PlayerPrefs.GetInt("UnlockedLevels", 0));
        //Check to see if there is any save data in the old PlayerPrefs format.
        //If there is, the game checks to see if the PlayerPrefs save data has the "SaveDataVersion" key.
        //If there is not PlayerPrefs save data, the game checks if there is savedata in the JSON format.
        //if (PlayerPrefs.HasKey("UnlockedLevels"))
        //{
        //    Debug.Log("UnlockedLevels exists.");
            //Checks to see if the PlayerPrefs save data has the "SaveDataVersion" key.
            //If it does not, that means that the save data is from a version prior to the level redesign (release 2.1) and the data must be reset.
            //If it does, the save data will be converted to JSON and the PlayerPrefs save data keys will be deleted.
            //if (!PlayerPrefs.HasKey("SaveDataVersion"))
            //{
            //    Debug.Log("SaveDataVersion does not exist");
            //    OldSaveNotification.SetActive(true);
            //    MainMenuUI.SetActive(false);
            //} else
            //{
            //    ConvertPlayerPrefsSaveDataToJson();
            //}
        //} else
        //{
            //Checks to see if there is save data in the JSON format.
            //If there is, nothing happens.
            //If there is not, the game creates a new JSON save data file.
        if (!System.IO.File.Exists(Application.persistentDataPath + "/save.json"))
        {
            UnlockedLevels = 1;
            PlayerColor = 1;
            SaveDataVersion = 2;
            MusicVolume = 1f;
            SaveData();
        }
        LoadData();
        //}
    }

    public void LoadData()
    {
        string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/save.json");
        JsonSaveDataClass SaveData = JsonUtility.FromJson<JsonSaveDataClass>(json);
        SaveDataVersion = SaveData.SaveDataVersion;
        PlayerColor = SaveData.PlayerColor;
        UnlockedLevels = SaveData.UnlockedLevels;
        MusicVolume = SaveData.MusicVolume;
    }
    public void SaveData()
    {
        JsonSaveDataClass SaveData = new JsonSaveDataClass
        {
            SaveDataVersion = SaveDataVersion,
            PlayerColor = PlayerColor,
            UnlockedLevels = UnlockedLevels,
            MusicVolume = MusicVolume
        };
        string json = JsonUtility.ToJson(SaveData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/save.json", json);
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
        LevelSelectUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void LevelSelectionChanged()
    {
        if (levelSelectDropdown.value > UnlockedLevels)
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
        if (levelSelectDropdown.value != 0)
        {
            if (levelSelectDropdown.value <= UnlockedLevels)
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

    public void OldSaveOKButton()
    {
        PlayerPrefs.DeleteKey("UnlockedLevels");
        PlayerPrefs.SetInt("SaveDataVersion", PlayerPrefs.GetInt("CurrentSaveDataVersion"));
        PlayerPrefs.Save();
        OldSaveNotification.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    
}
