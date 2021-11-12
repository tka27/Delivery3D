using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] StaticData staticData;
    [SerializeField] Text totalMoney;
    [SerializeField] GameObject demoCam;
    [SerializeField] MainMenuSceneData mainMenuSceneData;
    void Start()
    {

        staticData.carPerks = new int[staticData.allCars.Count][];

        for (int i = 0; i < staticData.allCars.Count; i++)
        {
            
            staticData.carPerks[i] = new int[5];
        }



        mainMenuSceneData.carInfoUpdateRequest = true;
        mainMenuSceneData.upgradesUpdateRequest = true;



        demoCam.SetActive(false);
        LoadGameProgress();
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
}
