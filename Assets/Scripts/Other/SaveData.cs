using UnityEngine;

[System.Serializable]
public class SaveData
{
    [System.NonSerialized] public StaticData staticData = (StaticData)Resources.Load("StaticData");
    public float totalMoney;
    public int researchLvl;
    public bool[] carsUnlockStatus;
    public bool[] carsBuyStatus;
    public bool[] trailersBuyStatus;
    public int[][] carPerks;
    public SaveData()
    {
        totalMoney = staticData.totalMoney;
        carPerks = staticData.carPerks;
        researchLvl = staticData.researchLvl;



        carsUnlockStatus = staticData.carsUnlockStatus;
        carsBuyStatus = staticData.carsBuyStatus;
        trailersBuyStatus = staticData.trailersBuyStatus;
    }
}
