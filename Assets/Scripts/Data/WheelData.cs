using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelData : MonoBehaviour
{
    public bool onRoad;
    bool firstCheck;
    string tag1 = "Road";
    string function = "OnRoadCheck";
    /*void Start()
    {
        tag1 = "Road";
        function = "OnRoadCheck";
    }*/
    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == tag1)
        {
            onRoad = true;
            firstCheck = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == tag1)
        {
            firstCheck = false;
            Invoke(function, 0.02f);
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
