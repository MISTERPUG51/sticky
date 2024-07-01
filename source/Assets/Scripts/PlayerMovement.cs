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
    public menubuttoncontrol menubuttoncontrol;
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



    private void Awake()
    {
        PlayerPrefs.Save();
        if (LevelNumber > PlayerPrefs.GetInt("UnlockedLevels", 0))
        {
            Debug.Log("unlocked new level");
            PlayerPrefs.SetInt("SaveDataVersion", PlayerPrefs.GetInt("CurrentSaveDataVersion"));
            PlayerPrefs.SetInt("UnlockedLevels", LevelNumber);
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.HasKey("PlayerColor"))
        {
            if (PlayerPrefs.GetInt("PlayerColor") == 1)
            {
                playerRenderer.material = material1;
            }
            if (PlayerPrefs.GetInt("PlayerColor") == 2)
            {
                playerRenderer.material = material2;
            }
            if (PlayerPrefs.GetInt("PlayerColor") == 3)
            {
                playerRenderer.material = material3;
            }
            if (PlayerPrefs.GetInt("PlayerColor") == 4)
            {
                playerRenderer.material = material4;
            }
            if (PlayerPrefs.GetInt("PlayerColor") == 5)
            {
                playerRenderer.material = material5;
            }
            if (PlayerPrefs.GetInt("PlayerColor") == 6)
            {
                playerRenderer.material = material6;
            }
            if (PlayerPrefs.GetInt("PlayerColor") == 7)
            {
                playerRenderer.material = material7;
            }
            if (PlayerPrefs.GetInt("PlayerColor") == 8)
            {
                playerRenderer.material = material8;
            }
            if (PlayerPrefs.GetInt("PlayerColor") == 9)
            {
                playerRenderer.material = material9;
            }
            if (PlayerPrefs.GetInt("PlayerColor") == 10)
            {
                playerRenderer.material = material10;
            }
            if (PlayerPrefs.GetInt("PlayerColor") == 11)
            {
                playerRenderer.material = material11;
            }
        }
        LogFPS();
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
        if (Input.GetKey("escape"))
        {
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            MovementEnabled = false;
            menubuttoncontrol.PauseGame();
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
            Invoke("LoadNextLevel", NextLevelDelay);
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
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        MovementEnabled = true;
    }

    public void LogFPS()
    {
        Debug.Log("FPS: " + (1f / Time.unscaledDeltaTime));
        Invoke("LogFPS", 0.5f);
    }
}
