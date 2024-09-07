using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsUIController : MonoBehaviour
{
    //This script should only be used to hide the default controls UI in Level1.
    public GameObject Controls;

    public void HideControls()
    {
        Controls.SetActive(false);
    }
}
