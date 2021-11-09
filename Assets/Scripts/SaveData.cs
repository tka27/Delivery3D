using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [System.NonSerialized] public StaticData staticData = (StaticData)Resources.Load("StaticData");
    public float totalMoney;
    public float labRequiredProductsMultiplier; 
    public List<bool> carsUnlockStatus;
    public List<bool> carsBuyStatus;
    public List<bool> buildingsUnlockStatus;
    public List<bool> buildingsBuyStatus;
    public SaveData()
    {
        totalMoney = staticData.totalMoney;
        labRequiredProductsMultiplier = staticData.labRequiredProductsMultiplier;
        carsUnlockStatus = staticData.carsUnlockStatus;
        carsBuyStatus = staticData.carsBuyStatus;
        buildingsUnlockStatus = staticData.buildingsUnlockStatus;
        buildingsBuyStatus = staticData.buildingsBuyStatus;
    }
}
