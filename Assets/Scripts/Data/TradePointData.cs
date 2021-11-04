using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradePointData : MonoBehaviour
{
    public Text storageInfo;
    public bool ableToTrade;
    string playerTag = "Player";


    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == playerTag)
        {
            ableToTrade = true;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == playerTag)
        {
            ableToTrade = false;
        }
    }
}
