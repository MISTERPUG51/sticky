using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeControl : MonoBehaviour
{
    public AudioSource MusicSource;
    // Start is called before the first frame update
    void Awake()
    {
        MusicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
    }
}
