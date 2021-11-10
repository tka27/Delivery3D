using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] StaticData staticData;
    [SerializeField] Text text;
    [SerializeField] GameObject demoCam;
    public Button button;
    void Start()
    {
        demoCam.SetActive(false);
        LoadGameProgress();
        text.text = staticData.totalMoney.ToString("0.0");
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
