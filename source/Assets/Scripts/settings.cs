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
    public SaveHandler SaveHandler;


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
        SceneManager.LoadScene("Update");
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
        Debug.Log("color=" + PlayerPrefs.GetInt("PlayerColor"));
    }

    public void ChangeMusicVolume()
    {
        SaveHandler.MusicVolume = MusicVolumeSlider.value;
    }
}
