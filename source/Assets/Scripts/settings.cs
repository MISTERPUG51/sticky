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
    public GameObject MainSettings;
    public GameObject ControlsSettings;

    public TMP_Text ForwardKeyText;
    public TMP_Text BackwardKeyText;
    public TMP_Text LeftKeyText;
    public TMP_Text RightKeyText;
    public TMP_Text PauseKeyText;

    public bool waitingForInput = false;
    public string KeybindBeingChanged;
    public KeyCode LastKeyPresed;

    private void Update()
    {
        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), SaveHandler.MoveForwardKey, true)))
        {
            UnityEngine.Debug.Log("Forward key pressed");
        }


        if (waitingForInput)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    waitingForInput = false;
                    LastKeyPresed = vKey;
                    if (KeybindBeingChanged == "moveForward")
                    {
                        SaveHandler.MoveForwardKey = vKey.ToString();
                        ForwardKeyText.text = vKey.ToString();
                    }

                    if (KeybindBeingChanged == "moveBackward")
                    {
                        SaveHandler.MoveBackwardKey = vKey.ToString();
                        BackwardKeyText.text = vKey.ToString();
                    }

                    if (KeybindBeingChanged == "moveLeft")
                    {
                        SaveHandler.MoveLeftKey = vKey.ToString();
                        LeftKeyText.text = vKey.ToString();
                    }

                    if (KeybindBeingChanged == "moveRight")
                    {
                        SaveHandler.MoveRightKey = vKey.ToString();
                        RightKeyText.text = vKey.ToString();
                    }

                    if (KeybindBeingChanged == "pause")
                    {
                        SaveHandler.PauseKey = vKey.ToString();
                        PauseKeyText.text = vKey.ToString();
                    }
                }
            }
        }
        
    }

    public void Start()
    {
        SaveHandler.LoadData();
        MusicVolumeSlider.value = SaveHandler.MusicVolume;
        PlayerCubeColorDropdown.value = SaveHandler.PlayerColor;
        ForwardKeyText.text = SaveHandler.MoveForwardKey;
        BackwardKeyText.text = SaveHandler.MoveBackwardKey;
        LeftKeyText.text = SaveHandler.MoveLeftKey;
        RightKeyText.text = SaveHandler.MoveRightKey;
        PauseKeyText.text = SaveHandler.PauseKey;
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

    public void ControlsMenu()
    {
        MainSettings.SetActive(false);
        ControlsSettings.SetActive(true);
    }

    public void ControlsOKButtonClicked()
    {
        MainSettings.SetActive(true);
        ControlsSettings.SetActive(false);
    }

    public void ChangeForwardKey()
    {
        ForwardKeyText.text = "Waiting for input.";
        KeybindBeingChanged = "moveForward";
        waitingForInput = true;
    }

    public void ChangeBackwardKey()
    {
        BackwardKeyText.text = "Waiting for input.";
        KeybindBeingChanged = "moveBackward";
        waitingForInput = true;
    }

    public void ChangeLeftKey()
    {
        LeftKeyText.text = "Waiting for input.";
        KeybindBeingChanged = "moveLeft";
        waitingForInput = true;
    }

    public void ChangeRightKey()
    {
        RightKeyText.text = "Waiting for input.";
        KeybindBeingChanged = "moveRight";
        waitingForInput = true;
    }

    public void ChangePauseKey()
    {
        PauseKeyText.text = "Waiting for input.";
        KeybindBeingChanged = "pause";
        waitingForInput = true;
    }


    public void ChangeMusicVolume()
    {
        SaveHandler.MusicVolume = MusicVolumeSlider.value;
    }
}
