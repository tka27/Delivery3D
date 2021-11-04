using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public UIData uiData;
    public void BuyClick()
    {
        uiData.buyRequest = true;
    }
}
