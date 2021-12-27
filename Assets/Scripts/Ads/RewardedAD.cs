using UnityEngine;
using UnityEngine.Advertisements;

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
        LoadAD();
    }

    void LoadAD()
    {
        Advertisement.Load(adUnit, this);
    }

    public void ShowAD()
    {
        if (!isLoaded)
        {
            LoadAD();
            return;
        }
        Advertisement.Show(adUnit, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("ad is loaded");
        if (placementId == adUnit)
        {
            isLoaded = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Ad load error: {error}-{message}");
        isLoaded = false;
    }





    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Ad show error: {error}-{message}");
    }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == adUnit && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            staticData.totalMoney += staticData.currentMoney / 2;
        }

        LoadAD();
    }
}
