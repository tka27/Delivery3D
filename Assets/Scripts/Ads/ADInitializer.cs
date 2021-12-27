using UnityEngine;
using UnityEngine.Advertisements;

public class ADInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    const string ANDROID_GAME_ID = "4492089";
    const string IOS_GAME_ID = "4492088";
    string gameID;
    bool testMode;

    private void Awake()
    {
        ADInit();
    }

    void ADInit()
    {
        testMode = Application.isEditor;
        Advertisement.Initialize(ANDROID_GAME_ID, testMode);

        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            gameID = IOS_GAME_ID;
        }
        else
        {
            gameID = ANDROID_GAME_ID;
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("AD initialized");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"AD init error: {error} - {message}");
    }
}
