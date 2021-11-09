using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu]
public class StaticData : ScriptableObject
{
    public List<GameObject> carsPrefabs;
    public float totalMoney;
    public float currentMoney;
    public float moneyForGame = 50;
    public float labRequiredProductsMultiplier;
    public int selectedCarID;
    public List<CarData> allCars;
    public List<bool> carsUnlockStatus;
    public List<bool> carsBuyStatus;
    public List<bool> buildingsUnlockStatus;
    public List<bool> buildingsBuyStatus;


    public void UpdateStaticData(SaveData data)
    {
        this.totalMoney = data.totalMoney;
        this.carsUnlockStatus = data.carsUnlockStatus;
    }
}

