using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradePointData : MonoBehaviour
{
    public Text storageInfo;
    public bool ableToTrade;
    string playerTag = "Player";
    public List<Text> buyPrice;
    public List<Text> buyCount;
    public Text sellCount;
    public Text sellPrice;
    public Text currentQuestTime;
    public SpriteRenderer buyProductSpriteRenderer;
    public SpriteRenderer SellProductSpriteRenderer;


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
