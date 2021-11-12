using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBtn : MonoBehaviour
{
    [SerializeField] GameObject playBtn;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject carInfoPanel;
    [SerializeField] GameObject buyBtn;
    [SerializeField] Transform moneyUpgradePos;
    [SerializeField] Transform moneyNormalPos;
    [SerializeField] Transform moneyGO;
    public void UpgradeBtnClick()
    {
        if (!upgradePanel.activeSelf)
        {
            upgradePanel.SetActive(true);
            playBtn.SetActive(false);
            carInfoPanel.SetActive(false);
            buyBtn.SetActive(true);
            moneyGO.position = moneyUpgradePos.position;
        }
        else
        {
            playBtn.SetActive(true);
            upgradePanel.SetActive(false);
            carInfoPanel.SetActive(true);
            buyBtn.SetActive(false);
            moneyGO.position = moneyNormalPos.position;
        }
    }
}
