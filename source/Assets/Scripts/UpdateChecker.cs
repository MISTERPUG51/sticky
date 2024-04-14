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
    public string CurrentArchitecture;
    public string UpdateDownloadUrl;
    public TMP_Text checkforupdatestext;
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
            checkforupdatestext.text = "Sticky was unable to check for updates. Press ALT + F4 to exit. Error details: " + www.error;
        }
        else
        {
            NewestVersion = www.downloadHandler.text;
        }
        CurrentVersion = Application.version;
        if (CurrentVersion==NewestVersion)
        {
            checkforupdatestext.text = "No updates are available. Returning to main menu.";
            Invoke("ReturnToMainMenu",2);
        } else
        {
            checkforupdatestext.text = "The latest update is being downloaded.";
            UpdateDownloadUrl = "https://github.com/MISTERPUG51/sticky/raw/main/binaries/1.5.1/x64/sticky_1.5.1_setup_win64.exe";
            Debug.Log("https://github.com/MISTERPUG51/sticky/raw/main/binaries/" + NewestVersion + "/" + CurrentArchitecture + "/sticky_" + NewestVersion + "_setup_win" + CurrentArchitecture + ".exe");
            if (CultureInfo.InvariantCulture.CompareInfo.IndexOf(SystemInfo.processorType, "ARM", CompareOptions.IgnoreCase) >= 0)
            {
                if (Environment.Is64BitProcess)
                    Debug.Log("How the fuck are rou doing this? This program was only desinged for x86-based processors!");
                else
                    Debug.Log("How the fuck are rou doing this? This program was only desinged for x86-based processors!");
            }
            else
            {
                // Must be in the x86 family.
                if (Environment.Is64BitProcess)
                    UpdateDownloadUrl = "https://raw.githubusercontent.com/MISTERPUG51/sticky/main/binaries/" + NewestVersion + "/x64/sticky_" + NewestVersion + "_setup_win64.exe";
                else
                    UpdateDownloadUrl = "https://raw.githubusercontent.com/MISTERPUG51/sticky/main/binaries/" + NewestVersion + "/x86/sticky_" + NewestVersion + "_setup_win32.exe";
            }
            StartCoroutine(DownloadUpdateFile());
            Debug.Log(UpdateDownloadUrl);
        }
    }
    IEnumerator DownloadUpdateFile()
    {
        UnityWebRequest www = UnityWebRequest.Get(UpdateDownloadUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            checkforupdatestext.text = "An update is available, but it could not be downloaded. Press ALT + F4 to exit. Error details: " + www.error;
        }
        else
        {
            FileStream fs = new FileStream(System.Environment.GetEnvironmentVariable("AppData") + "/StickyUpdater.exe", FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);
            w.Write(www.downloadHandler.data);
            w.Flush();
            w.Close();
            fs.Close();
            //UnityEngine.Windows.File.WriteAllBytes(System.Environment.GetEnvironmentVariable("AppData") + "/stickyUpdaterTemp/updater.exe", www.downloadHandler.data);
            checkforupdatestext.text = "Launching updater.";
            Invoke("StartUpdate", 3);
        }
    }

    public void DownloadCurrent()
    {
        
        //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(UpdateDownloadUrl);
        //webRequest.Method = "GET";
        //webRequest.Timeout = 10000;
        //webRequest.BeginGetResponse(new AsyncCallback(PlayResponeAsync), webRequest);
    }
    public void StartUpdate()
    {
        Application.OpenURL(System.Environment.GetEnvironmentVariable("AppData") + "/StickyUpdater.exe");
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
