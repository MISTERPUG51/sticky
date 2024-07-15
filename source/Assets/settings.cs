using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class settings : MonoBehaviour
{

    public TMP_Dropdown PlayerCubeColorDropdown;
    public Slider MusicVolumeSlider;


    public int UnlockedLevels;
    public int PlayerColor;
    public int SaveDataVersion;
    public float MusicVolume;


    [Serializable]
    public class JsonSaveDataClass
    {
        public int SaveDataVersion;
        public int PlayerColor;
        public int UnlockedLevels;
        public float MusicVolume;
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

    public void Start()
    {
        LoadData();
        MusicVolumeSlider.value = MusicVolume;
    }
    public void MainMenu()
    {
        SaveData();
        SceneManager.LoadScene("SampleScene");
    }

    public void CheckUpdate()
    {
        SceneManager.LoadScene("Update");
    }

    public void ResetSettings()
    {
        PlayerPrefs.DeleteKey("MusicVolume");
        PlayerPrefs.DeleteKey("PlayerColor");
        SceneManager.LoadScene("Settings");
    }

    public void DeleteProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("Settings");
    }

    public void PlayerCubeColorChanged()
    {

        PlayerPrefs.SetInt("PlayerColor", PlayerCubeColorDropdown.value);
        PlayerPrefs.Save();
        Debug.Log("color=" + PlayerPrefs.GetInt("PlayerColor"));
    }

    public void ChangeMusicVolume()
    {
        MusicVolume = MusicVolumeSlider.value;
    }
}
