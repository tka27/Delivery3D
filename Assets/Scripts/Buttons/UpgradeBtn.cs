using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBtn : MonoBehaviour
{
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject carInfoPanel;
    [SerializeField] Transform moneyUpgradePos;
    [SerializeField] Transform moneyNormalPos;
    [SerializeField] Transform moneyGO;
    [SerializeField] GameObject playBtn;
    public void UpgradeBtnClick()
    {
        if (!upgradePanel.activeSelf)
        {
            upgradePanel.SetActive(true);
            carInfoPanel.SetActive(false);
            moneyGO.position = moneyUpgradePos.position;
        }
        else
        {
            upgradePanel.SetActive(false);
            carInfoPanel.SetActive(true);
            moneyGO.position = moneyNormalPos.position;
        }
    }
}
