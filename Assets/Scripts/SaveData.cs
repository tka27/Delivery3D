using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [System.NonSerialized] public StaticData staticData = (StaticData)Resources.Load("StaticData");
    public float totalMoney;
    public List<bool> unlockedCars;
    public SaveData()
    {
        totalMoney = staticData.totalMoney;
        unlockedCars = staticData.unlockedCars;
    }
}
