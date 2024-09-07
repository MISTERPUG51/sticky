using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHandler : MonoBehaviour
{
    
    public int UnlockedLevels;
    public int PlayerColor;
    public int SaveDataVersion;
    public float MusicVolume;
    public string CreatedDate;
    public long Level1Time;
    public long Level2Time;
    public long Level3Time;
    public long Level4Time;
    public long Level5Time;
    public long Level6Time;

    // Start is called before the first frame update
    [Serializable]
    public class JsonSaveDataClass
    {
        public int SaveDataVersion;
        public int PlayerColor;
        public int UnlockedLevels;
        public float MusicVolume;
        public string CreatedDate;
        public long Level1Time;
        public long Level2Time;
        public long Level3Time;
        public long Level4Time;
        public long Level5Time;
        public long Level6Time;
    }

    public void UpdateSaveData()
    {
        LoadData();
        if (SaveDataVersion == 0)
        {
            PlayerColor = 9;
            UnlockedLevels = 1;
            MusicVolume = 1f;
            SaveDataVersion = 2;
            Debug.Log("Creating initial save (version 0)");
        }
        if (SaveDataVersion < 3)
        {
            Debug.Log("Converting save to version 3");
            SaveDataVersion = 3;
            CreatedDate = System.DateTime.Now.ToString();
            Level1Time = 0;
            Level2Time = 0;
            Level3Time = 0;
            Level4Time = 0;
            Level5Time = 0;
            Level6Time = 0;
        }
        SaveData();
    }

    public void LoadData()
    {
        string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/save.json");
        JsonSaveDataClass SaveData = JsonUtility.FromJson<JsonSaveDataClass>(json);
        SaveDataVersion = SaveData.SaveDataVersion;
        PlayerColor = SaveData.PlayerColor;
        UnlockedLevels = SaveData.UnlockedLevels;
        MusicVolume = SaveData.MusicVolume;
        CreatedDate = SaveData.CreatedDate;
        Level1Time = SaveData.Level1Time;
        Level2Time = SaveData.Level2Time;
        Level3Time = SaveData.Level3Time;
        Level4Time = SaveData.Level4Time;
        Level5Time = SaveData.Level5Time;
        Level6Time = SaveData.Level6Time;
    }
    public void SaveData()
    {
        JsonSaveDataClass SaveData = new JsonSaveDataClass
        {
            SaveDataVersion = SaveDataVersion,
            PlayerColor = PlayerColor,
            UnlockedLevels = UnlockedLevels,
            MusicVolume = MusicVolume,
            CreatedDate = CreatedDate,
            Level1Time = Level1Time,
            Level2Time = Level2Time,
            Level3Time = Level3Time,
            Level4Time = Level4Time,
            Level5Time = Level5Time,
            Level6Time = Level6Time,
        };
        string json = JsonUtility.ToJson(SaveData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }
}
