using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Checkpoint : MonoBehaviour
{

    public float NextLevelDelay = 1f;
    public PlayerMovement movement;
    public string NextLevel = "SampleScene";
    public AudioSource MusicSource;

    void OnCollisionEnter (Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Checkpoint")
        {
            movement.enabled = false;
            Invoke("LoadNextLevel", NextLevelDelay);
        }
    }

    void LoadNextLevel ()
    {
        PlayerPrefs.SetFloat("MusicTime", MusicSource.time);
        PlayerPrefs.Save();
        SceneManager.LoadScene(NextLevel);
    }

}
