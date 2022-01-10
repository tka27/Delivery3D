using UnityEngine;
using UnityEngine.SceneManagement;

public class Garage : MonoBehaviour
{
    const string PLAYER_TAG = "Player";
    bool isLeaveFromGarage;
    [SerializeField] StaticData staticData;
    [SerializeField] GameObject garageCanvas;
    [SerializeField] GameObject adCanvas;
    public static Garage singleton;

    private void Awake()
    {
        singleton = this;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == PLAYER_TAG && isLeaveFromGarage)
        {
            isLeaveFromGarage = false;
            ShowAdCanvas();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == PLAYER_TAG)
        {
            isLeaveFromGarage = true;
        }
    }

    public void ToGarageConfirm()
    {
        staticData.currentMoney /= 2;
        UIData.UpdateUI();
        ShowAdCanvas();
    }

    void ShowAdCanvas()
    {
        if (RewardedAD.singleton.isLoaded || Application.isEditor)
        {
            if (garageCanvas != null)
            {
                garageCanvas.SetActive(false);
            }
            adCanvas.SetActive(true);
        }
        else
        {
            GarageEnterProcess();
        }
    }

    public void GarageEnterProcess()
    {
        Time.timeScale = 1;
        staticData.totalMoney += staticData.currentMoney;
        staticData.currentMoney = 0;
        SaveSystem.Save();
        SceneManager.LoadScene(0);
    }
}
