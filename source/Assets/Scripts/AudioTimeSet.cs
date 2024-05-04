using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTimeSet : MonoBehaviour
{
    public AudioSource MusicSource;
    // Start is called before the first frame update
    void Awake()
    {
        MusicSource.time = PlayerPrefs.GetFloat("MusicTime", 0.1f);
    }
}
