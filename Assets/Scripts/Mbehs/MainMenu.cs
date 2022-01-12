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
    [SerializeField] GameObject tutorialPanel;

    void Awake()
    {
        staticData.SetDefaultData();
        settings.LoadPrefs();
        LoadGameProgress();

        if (settings.tutorialLvl >= 0)
        {
            tutorialPanel.SetActive(true);
        }
        if (!StaticData.isUpdated)
        {
            StaticData.onStartup.Invoke();
            StaticData.isUpdated = true;
        }
    }

    private void Start()
    {
        staticNotificationPanel = notificationPanel;
        staticNotificationText = notificationText;
        demoCam.SetActive(false);

        totalMoney.text = staticData.totalMoney.ToString("0.0");
        carInfoUpdateEvent.Invoke();
    }

    void LoadGameProgress() //copy SaveData to staticData
    {
        SaveData data = SaveSystem.Load();
        if (data != null)
        {
            staticData.UpdateStaticData(data);
            return;
        }
    }



    public static void Notification(string notification)
    {
        staticNotificationPanel.SetActive(true);
        staticNotificationText.text = notification;
    }

}
