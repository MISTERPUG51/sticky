using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public bool MovementEnabled;
    public float force;
    public float RestartDelay;
    public AudioSource MusicSource;
    public float NextLevelDelay;
    public string NextLevel;
    public int LevelNumber;
    public MeshRenderer playerRenderer;
    public Material material1;
    public Material material2;
    public Material material3;
    public Material material4;
    public Material material5;
    public Material material6;
    public Material material7;
    public Material material8;
    public Material material9;
    public Material material10;
    public Material material11;



    public PauseMenu PauseMenu;

    public SaveHandler SaveHandler;


    





    private void Awake()
    {
        SaveHandler.LoadData();
        PlayerPrefs.Save();
        if (LevelNumber > SaveHandler.UnlockedLevels)
        {
            Debug.Log("unlocked new level");
            SaveHandler.UnlockedLevels = LevelNumber;
            SaveHandler.SaveData();
        }
            if (SaveHandler.PlayerColor == 0)
            {
                playerRenderer.material = material1;
            }
            if (SaveHandler.PlayerColor == 1)
            {
                playerRenderer.material = material2;
            }
            if (SaveHandler.PlayerColor == 2)
            {
                playerRenderer.material = material3;
            }
            if (SaveHandler.PlayerColor == 3)
            {
                playerRenderer.material = material4;
            }
            if (SaveHandler.PlayerColor == 4)
            {
                playerRenderer.material = material5;
            }
            if (SaveHandler.PlayerColor == 5)
            {
                playerRenderer.material = material6;
            }
            if (SaveHandler.PlayerColor == 6)
            {
                playerRenderer.material = material7;
            }
            if (SaveHandler.PlayerColor == 7)
            {
                playerRenderer.material = material8;
            }
            if (SaveHandler.PlayerColor == 8)
            {
                playerRenderer.material = material9;
            }
            if (SaveHandler.PlayerColor == 9)
            {
                playerRenderer.material = material10;
            }
            if (SaveHandler.PlayerColor == 10)
            {
                playerRenderer.material = material11;
            }
    }

    void FixedUpdate()
    {
        if (MovementEnabled)
        {
            if (Input.GetKey("d"))
            {
                rb.AddForce(force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }

            if (Input.GetKey("a"))
            {
                rb.AddForce(-force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }

            if (Input.GetKey("s"))
            {
                rb.AddForce(0, 0, -force * Time.deltaTime, ForceMode.VelocityChange);
            }

            if (Input.GetKey("w"))
            {
                rb.AddForce(0, 0, force * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
        
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Wall")
        {
            MovementEnabled = false;
            Invoke("Restart", RestartDelay);
        }
        if (collisionInfo.collider.tag == "Checkpoint")
        {
            MovementEnabled = false;
            PauseMenu.LevelCompleted();
            //Invoke("LoadNextLevel", NextLevelDelay);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Fire")
        {
            MovementEnabled = false;
            Invoke("Restart", RestartDelay);
        }
    }

    public void Restart()
    {
        PlayerPrefs.SetFloat("MusicTime", MusicSource.time);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        PlayerPrefs.SetFloat("MusicTime", MusicSource.time);
        PlayerPrefs.Save();
        SceneManager.LoadScene(NextLevel);
    }

    public void ResumeGame()
    {
        
    }
}
