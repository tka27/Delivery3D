using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] StaticData staticData;
    [SerializeField] Text totalMoney;
    [SerializeField] GameObject demoCam;
    [SerializeField] MainMenuSceneData mainMenuSceneData;
    static GameObject notificationStaticPanel;
    static Text notificationStaticText;
    [SerializeField] GameObject notificationPanel;
    [SerializeField] Text notificationText;
    [SerializeField] UnityEvent carInfoUpdateEvent;
    void Start()
    {
        int carsCount = staticData.allCars.Count;

        staticData.carsUnlockStatus = new bool[carsCount];
        staticData.carsBuyStatus = new bool[carsCount];
        staticData.trailersBuyStatus = new bool[carsCount];
        notificationStaticPanel = notificationPanel;
        notificationStaticText = notificationText;
        staticData.carPerks = new int[carsCount][];

        for (int i = 0; i < carsCount; i++)
        {
            staticData.carPerks[i] = new int[5];
        }



        demoCam.SetActive(false);
        LoadGameProgress();
        carInfoUpdateEvent.Invoke();
        totalMoney.text = staticData.totalMoney.ToString("0.0");
    }



    void LoadGameProgress() //copy SaveData to staticData
    {
        SaveData data = SaveSystem.Load();
        if (data != null)
        {
            staticData.UpdateStaticData(data);
        }
    }



    public static void Notification(string notification)
    {
        notificationStaticPanel.SetActive(true);
        notificationStaticText.text = notification;
    }
}
