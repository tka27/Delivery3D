using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBtn : MonoBehaviour
{
    [SerializeField] StaticData staticData;
    public void OnClick()
    {
        staticData.allCars[0].fuelLvl++;
    }
}
