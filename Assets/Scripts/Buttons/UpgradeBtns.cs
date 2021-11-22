using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBtns : MonoBehaviour
{
    [SerializeField] GameObject upgradeCanvas;
    [SerializeField] GameObject carInfoCanvas;
    [SerializeField] Transform moneyUpgradePos;
    [SerializeField] Transform moneyNormalPos;
    [SerializeField] Transform moneyGO;
    public void UpgradeBtnClick()
    {
        if (!upgradeCanvas.activeSelf)
        {
            upgradeCanvas.SetActive(true);
            carInfoCanvas.SetActive(false);
            moneyGO.position = moneyUpgradePos.position;
        }
        else
        {
            CloseBtnClick();
        }
    }
    public void CloseBtnClick()
    {
        upgradeCanvas.SetActive(false);
        carInfoCanvas.SetActive(true);
        moneyGO.position = moneyNormalPos.position;
    }
}
