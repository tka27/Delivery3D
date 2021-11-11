using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBtn : MonoBehaviour
{
    [SerializeField] GameObject playBtn;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject carInfoPanel;
    [SerializeField] GameObject buyBtn;
    [SerializeField] Transform moneyNormalPos;
    [SerializeField] Transform moneyGO;
    public void CloseBtnClick()
    {
        playBtn.SetActive(true);
        upgradePanel.SetActive(false);
        carInfoPanel.SetActive(true);
        buyBtn.SetActive(false);
        moneyGO.position = moneyNormalPos.position;
    }
}
