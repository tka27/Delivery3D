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
    public List<CarData> allCars;
    public List<bool> carsUnlockStatus;
    public List<bool> carsBuyStatus;
    public List<bool> buildingsUnlockStatus;
    public int[][] carPerks;    //[carID[perkID]] = lvl     | 0 - fuel | 1 - speed | 2 - acceleration | 3 - suspension | 4 - storage |


    public void UpdateStaticData(SaveData data)
    {
        this.totalMoney = data.totalMoney;
        this.carsUnlockStatus = data.carsUnlockStatus;
        this.carPerks = data.carPerks;
    }
}

