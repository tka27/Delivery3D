using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInfo : MonoBehaviour
{
    [HideInInspector] public float[] perksPrices = new float[5];
    readonly float[] perksPricesMultiplier = new float[] { 1, 1.2f, 1.4f, 1.6f, 1.8f };
    [SerializeField] StaticData staticData;
    [SerializeField] List<Text> lvls;
    [SerializeField] List<Text> prices;
    const float LVL_MULTIPLIER = 1.2f;



    public void CarUpgradeUpdate()
    {
        for (int i = 0; i < perksPrices.Length; i++)
        {
            int perkLvl = staticData.carPerks[staticData.selectedCarID][i];
            float carPrice = staticData.allCars[staticData.selectedCarID].price;
            perksPrices[i] = perksPricesMultiplier[i] * carPrice / 10 + perksPricesMultiplier[i] * carPrice / 10 * perkLvl * LVL_MULTIPLIER;
            lvls[i].text = "lvl : " + perkLvl.ToString();
            prices[i].text = perksPrices[i].ToString("0.0");
        }
    }

    public void MapUpgradeUpdate()
    {
        float defaultPrice = 100;
        for (int i = 0; i < perksPrices.Length; i++)
        {
            int perkLvl = staticData.mapPerks[staticData.selectedMapID][i];
            perksPrices[i] = perksPricesMultiplier[i] * defaultPrice + perksPricesMultiplier[i] * defaultPrice * perkLvl * LVL_MULTIPLIER;
            lvls[i].text = "lvl : " + perkLvl.ToString();
            prices[i].text = perksPrices[i].ToString("0.0");
        }
    }
}
