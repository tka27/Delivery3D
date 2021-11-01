using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelData : MonoBehaviour
{
    public bool onRoad;
    bool firstCheck;
    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Road")
        {
            onRoad = true;
            firstCheck = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Road")
        {
            firstCheck = false;
            Invoke("OnRoadCheck", 0.02f);
        }
    }
    void OnRoadCheck()
    {
        if (!firstCheck)
        {
            onRoad = false;
            print(onRoad);
        }
    }
}
