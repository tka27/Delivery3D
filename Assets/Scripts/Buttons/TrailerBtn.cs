using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailerBtn : MonoBehaviour
{
    [SerializeField] StaticData staticData;
    [SerializeField] CarInfo carInfo;

    public void TrailerSwitch()
    {
        staticData.trailerIsSelected = !staticData.trailerIsSelected;
        carInfo.InfoUpdate();
    }
}
