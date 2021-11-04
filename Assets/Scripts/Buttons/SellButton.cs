using UnityEngine;

public class SellButton : MonoBehaviour
{
    public UIData uiData;
    public void SellClick()
    {
        uiData.sellRequest = true;
    }
}
