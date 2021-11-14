using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCarBtn : MonoBehaviour
{
    [SerializeField] StaticData staticData;
    [SerializeField] CarInfo carInfo;


    public void BuyCar()
    {
        int carID = staticData.selectedCarID;
        if (!staticData.carsBuyStatus[carID])
        {
            if (staticData.totalMoney >= staticData.allCars[carID].price)
            {
                staticData.totalMoney -= staticData.allCars[carID].price;
                staticData.carsBuyStatus[carID] = true;
                carInfo.InfoUpdate();
            }
            else
            {
                MainMenu.Notification("Not enough coins");
            }
        }
        else if (!staticData.trailersBuyStatus[carID])
        {
            if (staticData.totalMoney >= staticData.allCars[carID].price / 2)
            {
                staticData.totalMoney -= staticData.allCars[carID].price / 2;
                staticData.trailersBuyStatus[carID] = true;
                carInfo.InfoUpdate();
            }
            else
            {
                MainMenu.Notification("Not enough coins");
            }
        }
    }
}
