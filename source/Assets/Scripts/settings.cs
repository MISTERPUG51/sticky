//using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics;

public class settings : MonoBehaviour
{

    public TMP_Dropdown PlayerCubeColorDropdown;
    public Slider MusicVolumeSlider;
    public SaveHandler SaveHandler;
    public TMP_Text TitleText;
    public TMP_Text UpdateIncompatibilityText;

    public void Start()
    {
        SaveHandler.LoadData();
        MusicVolumeSlider.value = SaveHandler.MusicVolume;
        PlayerCubeColorDropdown.value = SaveHandler.PlayerColor;
    }
    public void MainMenu()
    {
        SaveHandler.SaveData();
        SceneManager.LoadScene("SampleScene");
    }

    public void CheckUpdate()
    {
        string baseDir = System.AppDomain.CurrentDomain.BaseDirectory;
        if (System.IO.File.Exists(baseDir + "/updater.exe"))
        {
            UnityEngine.Debug.Log(baseDir);
            System.IO.File.Copy(baseDir + "/updater.exe", Application.persistentDataPath + "/updater.exe");
            System.Diagnostics.Process.Start(Application.persistentDataPath + "/updater.exe");
            Application.Quit();
        }
        else
        {
            TitleText.enabled = false;
            UpdateIncompatibilityText.enabled = true;
        }
        
    }

    public void ResetSettings()
    {
        SaveHandler.MusicVolume = 1f;
        SaveHandler.PlayerColor = 1;
        SaveHandler.SaveData();
        SceneManager.LoadScene("Settings");
    }

    public void PlayerCubeColorChanged()
    {
        SaveHandler.PlayerColor = PlayerCubeColorDropdown.value;
        UnityEngine.Debug.Log("color=" + PlayerPrefs.GetInt("PlayerColor"));
    }

    public void ChangeMusicVolume()
    {
        SaveHandler.MusicVolume = MusicVolumeSlider.value;
    }
}
