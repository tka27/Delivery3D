using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] FlowingText flowingText;
    [SerializeField] StaticData staticData;
    [SerializeField] UIData uiData;
    [SerializeField] SoundData soundData;
    string playerTag = "Player";
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == playerTag)
        {
            soundData.PlayCoin();
            gameObject.SetActive(false);
            staticData.currentMoney += 1 + staticData.researchLvl;
            flowingText.DisplayText("+" + (1 + staticData.researchLvl).ToString("0.0"));
            uiData.moneyText.text = staticData.currentMoney.ToString("0.0");
        }
    }
}
