using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] StaticData staticData;
    [SerializeField] GameSettings settings;
    [SerializeField] Text totalMoney;
    [SerializeField] GameObject demoCam;
    static GameObject staticNotificationPanel;
    static Text staticNotificationText;
    [SerializeField] GameObject notificationPanel;
    [SerializeField] Text notificationText;
    [SerializeField] UnityEvent carInfoUpdateEvent;
    
    void Start()
    {
        int carsCount = staticData.allCars.Count;
        int mapsCount = 1;

        staticData.carsUnlockStatus = new bool[carsCount];
        staticData.carsUnlockStatus[0] = true;
        staticData.carsBuyStatus = new bool[carsCount];
        staticData.carsBuyStatus[0] = true;
        staticData.trailersBuyStatus = new bool[carsCount];


        staticNotificationPanel = notificationPanel;
        staticNotificationText = notificationText;


        staticData.carPerks = new int[carsCount][];
        for (int i = 0; i < carsCount; i++)
        {
            staticData.carPerks[i] = new int[5];
        }

        staticData.mapPerks = new int[mapsCount][];
        for (int i = 0; i < mapsCount; i++)
        {
            staticData.mapPerks[i] = new int[5];
        }



        demoCam.SetActive(false);
        settings.LoadPrefs();
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
        staticNotificationPanel.SetActive(true);
        staticNotificationText.text = notification;
    }

}
