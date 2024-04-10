using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    public string Level = "SampleScene";

    void FixedUpdate()
    {
        if (Input.GetKey("1"))
        {
            if (Input.GetKey("2"))
            {
                if (Input.GetKey("3"))
                {
                    SceneManager.LoadScene(Level);
                }
            }
        }
    }

}
