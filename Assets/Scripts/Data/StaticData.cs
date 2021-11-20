using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu]
public class StaticData : ScriptableObject
{
    public List<GameObject> carsPrefabs;
    public List<Product> availableProducts;
    public float totalMoney;
    public float currentMoney;
    public float moneyForGame = 50;
    public int selectedCarID;
    public bool trailerIsSelected;
    public List<CarData> allCars;
    public int researchLvl;
    public bool[] carsUnlockStatus;
    public bool[] carsBuyStatus;
    public bool[] trailersBuyStatus;
    public int[][] carPerks;    //[carID[perkID]] = lvl     | 0 - fuel | 1 - speed | 2 - acceleration | 3 - suspension | 4 - storage |


    public void UpdateStaticData(SaveData data)
    {
        this.totalMoney = data.totalMoney;
        this.researchLvl = data.researchLvl;

        for (int i = 0; i < this.carsBuyStatus.Length; i++)
        {
            this.carsBuyStatus[i] = data.carsBuyStatus[i];
        }

        for (int i = 0; i < this.trailersBuyStatus.Length; i++)
        {
            this.trailersBuyStatus[i] = data.trailersBuyStatus[i];
        }

        for (int i = 0; i < this.carsUnlockStatus.Length; i++)
        {
            this.carsUnlockStatus[i] = data.carsUnlockStatus[i];
        }
        
        for (int i = 0; i < this.carPerks.Length; i++)
        {
            this.carPerks[i] = data.carPerks[i];
        }
    }
}

