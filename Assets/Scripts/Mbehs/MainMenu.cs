using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    void Awake()
    {
        staticNotificationPanel = notificationPanel;
        staticNotificationText = notificationText;
        demoCam.SetActive(false);

        staticData.SetDefaultData();
        settings.LoadPrefs();
        LoadGameProgress();
        carInfoUpdateEvent.Invoke();
        totalMoney.text = staticData.totalMoney.ToString("0.0");

        if (settings.tutorialLvl >= 0)
        {
            staticData.selectedCarID = 2;
            staticData.trailerIsSelected = true;
            SceneManager.LoadScene(1);
        }
    }



    void LoadGameProgress() //copy SaveData to staticData
    {
        SaveData data = SaveSystem.Load();
        if (data != null)
        {
            staticData.UpdateStaticData(data);
            return;
        }
        settings.SetDefaultGraphics();
    }



    public static void Notification(string notification)
    {
        staticNotificationPanel.SetActive(true);
        staticNotificationText.text = notification;
    }

}
