using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseInit : MonoBehaviour
{
    void Awake()
    {
        StaticData.onStartup += Init;
    }
    private void OnDestroy()
    {
        StaticData.onStartup -= Init;
    }

    void Init()
    {
        Debug.Log("Firebase init");
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        });
    }
}
