using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        if (LevelNumber > PlayerPrefs.GetInt("UnlockedLevels", 0))
        {
            Debug.Log("unlocked new level");
            PlayerPrefs.SetInt("UnlockedLevels", LevelNumber);
            PlayerPrefs.Save();
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
}
