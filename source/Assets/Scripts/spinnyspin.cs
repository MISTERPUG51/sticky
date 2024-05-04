using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinnyspin : MonoBehaviour
{
    public Rigidbody SpinnyRigidbody;
    Vector3 m_EulerAngleVelocity;
    public int XSpinSpeed;
    public int YSpinSpeed;
    public int ZSpinSpeed;
    private void Start()
    {
        m_EulerAngleVelocity = new Vector3(XSpinSpeed, YSpinSpeed, ZSpinSpeed);
    }
    private void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
        SpinnyRigidbody.MoveRotation(SpinnyRigidbody.rotation * deltaRotation);
    }
}
