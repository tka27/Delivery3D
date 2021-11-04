using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    UIData uiData;
    void BuyClick()
    {
        uiData.buyRequest = true;
    }
}
