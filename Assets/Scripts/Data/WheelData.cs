using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelData : MonoBehaviour
{
    public bool onRoad;
    public bool inWater;
    bool firstCheck;
    string roadTag = "Road";
    string waterTag = "Water";
    string function = "OnRoadCheck";
    public ParticleSystem particles;
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == waterTag)
        {
            inWater = true;
        }
    }
    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == roadTag)
        {
            onRoad = true;
            firstCheck = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == roadTag)
        {
            firstCheck = false;
            Invoke(function, 0.02f);
        }
        if (collider.tag == waterTag)
        {
            inWater = false;
        }
    }
    void OnRoadCheck()
    {
        if (!firstCheck)
        {
            onRoad = false;
        }
    }
}
