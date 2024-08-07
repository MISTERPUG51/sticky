using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeControl : MonoBehaviour
{
    public AudioSource MusicSource;
    // Start is called before the first frame update
    void Awake()
    {
        LoadData();
        MusicSource.volume = MusicVolume;
    }

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
}
