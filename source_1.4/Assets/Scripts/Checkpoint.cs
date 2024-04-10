using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{

    public float NextLevelDelay = 1f;
    public PlayerMovement movement;
    public string NextLevel = "SampleScene";

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
        SceneManager.LoadScene(NextLevel);
    }

}
