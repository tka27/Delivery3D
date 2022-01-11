using Firebase.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class BuyCarBtn : MonoBehaviour
{
    [SerializeField] StaticData staticData;
    [SerializeField] CarInfo carInfo;
    [SerializeField] Text totalMoney;
    [SerializeField] GameObject rateCanvas;


    public void BuyCar()
    {
        SoundData.PlayBtn();
        int carID = staticData.selectedCarID;
        if (!staticData.carsBuyStatus[carID])
        {
            if (staticData.totalMoney >= staticData.allCars[carID].price)
            {
                staticData.totalMoney -= staticData.allCars[carID].price;
                staticData.carsBuyStatus[carID] = true;
                carInfo.InfoUpdate();
                SoundData.PlayCoin();
                if (carID == 1)
                {
                    rateCanvas.SetActive(true);

                    FirebaseAnalytics.LogEvent("Buy_car_1");
                }
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
                SoundData.PlayCoin();
            }
            else
            {
                MainMenu.Notification("Not enough coins");
            }
        }
        totalMoney.text = staticData.totalMoney.ToString("0");
    }
}
