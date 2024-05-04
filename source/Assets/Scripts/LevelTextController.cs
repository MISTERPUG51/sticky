using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTextController : MonoBehaviour
{
    public GameObject LevelText;
    public float LevelTextDisableDelay;
    private void Start()
    {
        LevelText.SetActive(true);
        Invoke("DisableLevelText", LevelTextDisableDelay);
    }
    public void DisableLevelText()
    {
        LevelText.SetActive(false);
    }
}
