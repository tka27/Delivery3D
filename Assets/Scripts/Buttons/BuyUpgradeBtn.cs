using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyUpgradeBtn : MonoBehaviour
{
    [SerializeField] MainMenuSceneData mainMenuSceneData;
    [SerializeField] StaticData staticData;
    [SerializeField] BuyUpgradeInfo buyUpgradeInfo;
    [SerializeField] Text totalMoney;
    public void BuyUpgrade()
    {
        if (staticData.totalMoney > buyUpgradeInfo.perksPrices[mainMenuSceneData.selectedPerkID])
        {
            staticData.totalMoney -= buyUpgradeInfo.perksPrices[mainMenuSceneData.selectedPerkID];
            staticData.carPerks[staticData.selectedCarID][mainMenuSceneData.selectedPerkID]++; //selected perk for current car

            totalMoney.text = staticData.totalMoney.ToString("0");
            mainMenuSceneData.upgradesUpdateRequest = true;
        }
    }

}
