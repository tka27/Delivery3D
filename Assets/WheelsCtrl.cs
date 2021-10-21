using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsCtrl : MonoBehaviour
{
    public List<WheelCollider> wheels;
    public Transform centerOfMass;
    public List<Transform> wheelMeshes;
    float maxSteerDegrees = 45;
    float maxTorque = 2500;
    float steer;

    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass.transform.localPosition;
    }

    void Update()
    {
        steer = maxSteerDegrees * Input.GetAxis("Horizontal");
        foreach (var wheel in wheels)
        {
            wheel.motorTorque = maxTorque * Input.GetAxis("Vertical");
        }

    }
    void FixedUpdate()
    {
        for (int i = 0; i < wheels.Count; i++)
        {
            if (i < 2)
            {
                wheels[i].steerAngle = steer;
            }
            Vector3 pos;
            Quaternion quaternion;
            wheels[i].GetWorldPose(out pos, out quaternion);
            wheelMeshes[i].transform.position = pos;
            wheelMeshes[i].transform.rotation = quaternion;

        }
    }
}
