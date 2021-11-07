using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu]
public class StaticData : ScriptableObject
{
    public float totalMoney;
    public float currentMoney;
    public float moneyForGame = 50;
    public List<bool> unlockedCars;
}

