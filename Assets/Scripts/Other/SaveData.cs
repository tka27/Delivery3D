using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [System.NonSerialized] public StaticData staticData = (StaticData)Resources.Load("StaticData");
    public float totalMoney;
    public int researchLvl;
    public List<bool> carsUnlockStatus;
    public List<bool> carsBuyStatus;
    public List<bool> buildingsUnlockStatus;
    public int[][] carPerks;
    public SaveData()
    {
        totalMoney = staticData.totalMoney;
        carPerks = staticData.carPerks;
        researchLvl = staticData.researchLvl;



        carsUnlockStatus = staticData.carsUnlockStatus;
        carsBuyStatus = staticData.carsBuyStatus;
        buildingsUnlockStatus = staticData.buildingsUnlockStatus;
    }
}
