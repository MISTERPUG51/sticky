using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Net;
using System;
using System.Globalization;
using static System.Net.WebRequestMethods;
using UnityEngine.SceneManagement;

public class UpdateChecker : MonoBehaviour
{
    public string CurrentVersion;
    public string NewestVersion;
    public TMP_Text checkforupdatestext;
    public GameObject DownloadButton;
    public GameObject QuitButton;
    public GameObject InfoText;
    public void Awake()
    {
        CheckForUpdates();
    }
    public void CheckForUpdates()
    {
        StartCoroutine(GetCurrentVersion());
    }
    IEnumerator GetCurrentVersion()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://raw.githubusercontent.com/MISTERPUG51/sticky/main/version.txt");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            checkforupdatestext.text = "An error occurred while checking for updates. Error details: " + www.error;
        }
        else
        {
            NewestVersion = www.downloadHandler.text;
        }
        CurrentVersion = Application.version;
        if (CurrentVersion==NewestVersion)
        {
            checkforupdatestext.text = "No updates are available.";
        } else
        {
            checkforupdatestext.text = "An update is available.";
            Invoke("MakeDownloadButtonActive",0);
        }
    }

    public void MakeDownloadButtonActive()
    {
        DownloadButton.SetActive(true);
        QuitButton.SetActive(true);
        InfoText.SetActive(true);
    }

    public void OpenDownloadPage()
    {
        Application.OpenURL("https://github.com/MISTERPUG51/sticky/releases/latest");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
