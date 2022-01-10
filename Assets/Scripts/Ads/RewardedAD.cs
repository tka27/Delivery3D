using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using Firebase.Analytics;

public class RewardedAD : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] StaticData staticData;
    const string ANDROID_AD_UNIT = "Rewarded_Android";
    const string IOS_AD_UNIT = "Rewarded_iOS";
    string adUnit;

    public static RewardedAD singleton;
    public bool isLoaded { get; private set; }

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
        StartCoroutine(LoadAd());
    }

    public void ShowAD()
    {
        if (!isLoaded)
        {
            StartCoroutine(LoadAd());
            return;
        }
        Advertisement.Show(adUnit, this);
        isLoaded = false;
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId == adUnit)
        {
            isLoaded = true;
            Debug.Log("Rewarded ad is loaded");
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        if (placementId == adUnit)
        {
            Debug.LogError($"Ad load error: {error}-{message}");
            isLoaded = false;
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Ad show error: {error}-{message}");
    }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Ad show is over: " + adUnit + "|" + placementId + "|" + showCompletionState);
        if (placementId == adUnit)
        {
            FirebaseAnalytics.LogEvent("Interstitial ad show");
            StartCoroutine(EnterTheGarage(showCompletionState));
        }
    }

    IEnumerator EnterTheGarage(UnityAdsShowCompletionState showCompletionState)
    {
        yield return new WaitForEndOfFrame();
        if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            staticData.totalMoney += staticData.currentMoney / 2;
        }

        Garage.singleton.GarageEnterProcess();
        StartCoroutine(LoadAd());
    }

    IEnumerator LoadAd()
    {
        yield return new WaitForEndOfFrame();
        Advertisement.Load(adUnit, this);
    }
}
