using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public menubuttoncontrol menubuttoncontrol;
    public PlayerMovement PlayerMovement;
    public Rigidbody PlayerRigidBody;
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            PlayerRigidBody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            PlayerMovement.enabled = false;
            menubuttoncontrol.PauseGame();
        }
    }
    public void ResumeGame()
    {
        PlayerRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        PlayerMovement.enabled = true;
    }
}
