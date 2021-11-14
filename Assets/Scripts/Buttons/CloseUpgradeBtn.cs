using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpgradeBtn : MonoBehaviour
{
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject carInfoPanel;
    [SerializeField] Transform moneyNormalPos;
    [SerializeField] Transform moneyGO;
    public void CloseBtnClick()
    {
        upgradePanel.SetActive(false);
        carInfoPanel.SetActive(true);
        moneyGO.position = moneyNormalPos.position;
    }
}
