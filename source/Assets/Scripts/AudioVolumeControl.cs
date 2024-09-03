using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeControl : MonoBehaviour
{
    public AudioSource MusicSource;
    public SaveHandler SaveHandler;
    // Start is called before the first frame update
    void Awake()
    {
        SaveHandler.LoadData();
        MusicSource.volume = SaveHandler.MusicVolume;
    }
}
