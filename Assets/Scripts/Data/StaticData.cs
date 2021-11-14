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
        this.carsBuyStatus = data.carsBuyStatus;
        this.trailersBuyStatus = data.trailersBuyStatus;
        this.carsUnlockStatus = data.carsUnlockStatus;
        this.carPerks = data.carPerks;
    }
}

