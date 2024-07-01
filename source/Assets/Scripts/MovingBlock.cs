using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingBlock : MonoBehaviour
{
    public Rigidbody BlockRigidbody;
    public BoxCollider BoxCollider;
    public float Phase1Time;
    public float Phase2Time;
    public float Xforce;
    public float Yforce;
    public float Zforce;
    public float XActiveForce;
    public float YActiveForce;
    public float ZActiveForce;
    public float StartOffset;
    public AudioSource moveSound;
    public AudioSource thumpSound;
    public AudioSource splatSound;

    public void Start()
    {
        Invoke("Phase1", StartOffset);
    }

    public void FixedUpdate()
    {
        BlockRigidbody.AddForce(XActiveForce * Time.deltaTime, YActiveForce * Time.deltaTime, ZActiveForce * Time.deltaTime, ForceMode.VelocityChange);
    }

    public void Phase1()
    {
        moveSound.enabled = true;
        thumpSound.enabled = false;
        XActiveForce = Xforce;
        YActiveForce = Yforce;
        ZActiveForce = Zforce;
        Invoke("Phase2", Phase1Time);
    }

    public void Phase2()
    {
        moveSound.enabled = true;
        thumpSound.enabled = false;
        XActiveForce = -Xforce;
        YActiveForce = -Yforce;
        ZActiveForce = -Zforce;
        Invoke("Phase1", Phase2Time);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Wall")
        {
            moveSound.enabled = false;
            thumpSound.enabled = true;
        }
        if (collision.collider.tag == "Player")
        {
            //moveSound.enabled = false;
            //splatSound.enabled = true;
        }
    }
}
