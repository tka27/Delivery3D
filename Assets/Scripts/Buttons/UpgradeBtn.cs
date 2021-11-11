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
    [SerializeField] Transform moneyGO;
    public void UpgradeBtnClick()
    {
        playBtn.SetActive(false);
        upgradePanel.SetActive(true);
        carInfoPanel.SetActive(false);
        buyBtn.SetActive(true);
        moneyGO.position = moneyUpgradePos.position;
    }
}
