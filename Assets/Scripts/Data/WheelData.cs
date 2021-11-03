using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelData : MonoBehaviour
{
    public bool onRoad;
    bool firstCheck;
    string tag1;
    string tag2;
    string tag3;
    string function;
    void Start()
    {
        tag1 = "Road";
        tag2 = "FinalPoint";
        tag3 = "Farm";
        function = "OnRoadCheck";
    }
    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == tag1 || collider.tag == tag2||collider.tag == tag3)
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
