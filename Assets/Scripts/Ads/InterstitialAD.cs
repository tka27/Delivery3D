using Firebase.Analytics;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAD : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    const string ANDROID_AD_UNIT = "Interstitial_Android";
    const string IOS_AD_UNIT = "Interstitial_iOS";
    string adUnit;

    public static InterstitialAD singleton;

    private void Awake()
    {
        singleton = this;
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            adUnit = IOS_AD_UNIT;
        }
        else
        {
            adUnit = ANDROID_AD_UNIT;
        }
    }

    private void Start()
    {
        LoadAD();
    }

    void LoadAD()
    {
        Advertisement.Load(adUnit, this);
    }

    public void ShowAD()
    {
        Advertisement.Show(adUnit, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Interstitial ad is loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Ad load error: {error}-{message}");
    }





    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Ad show error: {error}-{message}");
    }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Interstitial ad show is over: " + adUnit + "|" + placementId + "|" + showCompletionState);

        FirebaseAnalytics.LogEvent("Interstitial_ad_show");
        LoadAD();
    }

}
