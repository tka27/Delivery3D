using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] FlowingText flowingText;
    [SerializeField] StaticData staticData;
    [SerializeField] UIData uiData;
    [SerializeField] ParticleSystem particles;
    string playerTag = "Player";
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == playerTag)
        {
            SoundData.PlayCoin();
            particles.transform.position = transform.position;
            particles.Play();
            gameObject.SetActive(false);
            staticData.currentMoney += 1 + staticData.researchLvl;
            flowingText.DisplayText("+" + (1 + staticData.researchLvl).ToString("0.0"));
            uiData.moneyText.text = staticData.currentMoney.ToString("0.0");
        }
    }
}
