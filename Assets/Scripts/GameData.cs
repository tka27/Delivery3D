using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    [SerializeField] StaticData staticData;
    [SerializeField] Text text;
    void Start()
    {
        text.text = staticData.totalMoney.ToString("0");
    }
}
