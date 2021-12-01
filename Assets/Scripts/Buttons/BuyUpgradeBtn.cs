using UnityEngine;
using UnityEngine.UI;

public class BuyUpgradeBtn : MonoBehaviour
{
    [SerializeField] MainMenuSceneData mainMenuSceneData;
    [SerializeField] StaticData staticData;
    [SerializeField] UpgradeInfo buyUpgradeInfo;
    [SerializeField] Text totalMoney;
    [SerializeField] FlowingText flowingText;
    public void BuyUpgrade()
    {
        SoundData.PlayBtn();
        if (staticData.totalMoney > buyUpgradeInfo.perksPrices[mainMenuSceneData.selectedPerkID])
        {
            float perkCost = buyUpgradeInfo.perksPrices[mainMenuSceneData.selectedPerkID];
            staticData.totalMoney -= perkCost;
            staticData.carPerks[staticData.selectedCarID][mainMenuSceneData.selectedPerkID]++; //selected perk for current car

            totalMoney.text = staticData.totalMoney.ToString("0");
            buyUpgradeInfo.UpgradeUpdate();
            SoundData.PlayCoin();
        }
        else
        {
            MainMenu.Notification("Not enough money");
        }
    }

}
