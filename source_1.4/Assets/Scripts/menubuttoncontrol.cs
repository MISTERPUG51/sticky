using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menubuttoncontrol : MonoBehaviour
{
    [SerializeField] private string startScene = "level01";
    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
    }
}
