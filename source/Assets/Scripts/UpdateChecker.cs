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
using LogicUI.FancyTextRendering;
using static SaveHandler;
using System.Text.RegularExpressions;
using UnityEngine.Windows;

public class UpdateChecker : MonoBehaviour
{
    public string CurrentVersion;
    public string NewestVersion;
    public GameObject DownloadButton;
    public GameObject QuitButton;
    public GameObject MainMenuButton;
    public GameObject InstallButton;
    public MarkdownRenderer MarkdownRenderer;

    [Serializable]
    public class releaseJSONClass
    {
        public string body;
    }



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
        MarkdownRenderer.Source = "Checking for updates...<br><br>Status: Downloading https://raw.githubusercontent.com/MISTERPUG51/sticky/main/version.txt";
        UnityWebRequest www = UnityWebRequest.Get("https://raw.githubusercontent.com/MISTERPUG51/sticky/main/version.txt");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            MarkdownRenderer.Source = "An error occurred while checking for updates. Error details: " + www.error;
        }
        else
        {
            NewestVersion = www.downloadHandler.text;
        }

        CurrentVersion = Application.version;
        MarkdownRenderer.Source = "Checking for updates...<br><br>Status: Comparing version numbers.";
        if (CurrentVersion==NewestVersion)
        {
            MarkdownRenderer.Source = "No updates are available.";
        } else
        {
            MarkdownRenderer.Source = "An update is available. Downloading update info.<br><br>Status: Downloading https://api.github.com/repos/MISTERPUG51/Sticky/releases/latest";
            UnityWebRequest downloadUpdateInfoWebRequest = UnityWebRequest.Get("https://api.github.com/repos/MISTERPUG51/Sticky/releases/latest");
            yield return downloadUpdateInfoWebRequest.SendWebRequest();
            if (downloadUpdateInfoWebRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(downloadUpdateInfoWebRequest.error);
            }
            else
            {
                string rawReleaseJSON = downloadUpdateInfoWebRequest.downloadHandler.text;
                MarkdownRenderer.Source = "An update is available. Downloading update info.<br><br>Status: Parsing JSON.";
                releaseJSONClass releaseJSON = JsonUtility.FromJson<releaseJSONClass>(rawReleaseJSON);
                MarkdownRenderer.Source = "An update is available. Downloading update info.<br><br>Status: Replacing line break characters.";
                string releaseBodyWithProperLineBreaks = Regex.Replace(releaseJSON.body, @"\r\n?|\n", Environment.NewLine);
                MarkdownRenderer.Source = "An update is available. You are running version " + CurrentVersion.ToString() + ". The latest version is " + NewestVersion.ToString() + ". The release notes for the newest version are below. <br><br>" + releaseBodyWithProperLineBreaks;
            }
            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                Debug.Log("Operating system is Windows");
            } else if (Application.platform == RuntimePlatform.LinuxPlayer)
            {
                Debug.Log("Operating system is Windows");
            }
            Invoke("MakeDownloadButtonActive",0);
        }
    }

    public void DownloadAndInstallUpdate()
    {
        Debug.Log("Application.dataPath is " + Application.dataPath);
        Debug.Log(System.IO.Directory.GetParent(Application.dataPath));
        if (System.IO.Directory.GetParent(Application.dataPath).ToString() == "C:\\Program Files\\Sticky")
        {
            MarkdownRenderer.Source = "Downloading installer.";
            Debug.Log("Program is installed.");
            Debug.Log("Temp path: " + Path.GetTempPath());
            StartCoroutine(urlDownload());
        }
        
    }

    public void StartUpdater()
    {
        Debug.Log(Path.GetTempPath() + "/stickyupdater.exe");
        Application.OpenURL(Path.GetTempPath() + "/stickyupdater.exe");
        Application.Quit();
    }

    IEnumerator urlDownload()
    {
        UnityWebRequest updaterWebRequest = UnityWebRequest.Get("https://github.com/MISTERPUG51/sticky/raw/main/updater/stickyupdater.exe");
        yield return updaterWebRequest.SendWebRequest();

        if (updaterWebRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(updaterWebRequest.error);
            MarkdownRenderer.Source = "An error has occured.";
        }
        else
        {
            MarkdownRenderer.Source = "Writing updater file.";
            FileStream fs = new FileStream(Path.GetTempPath() + "/stickyupdater.exe", FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);
            w.Write(updaterWebRequest.downloadHandler.data);
            w.Flush();
            w.Close();
            fs.Close();
            MarkdownRenderer.Source = "The installer will start in a few seconds.";
            Invoke("StartUpdater", 5);
        }
    }
    public void MakeDownloadButtonActive()
    {
        DownloadButton.SetActive(true);
        QuitButton.SetActive(true);
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            InstallButton.SetActive(true);
        }
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
