using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseInit : MonoBehaviour
{
    [SerializeField] StaticData staticData;
    void Awake()
    {
        if (staticData.firebaseIsInit || Application.isEditor) return;

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                staticData.firebaseIsInit = true;
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            });
    }
}
