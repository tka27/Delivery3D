using UnityEngine;

public class ClearInventoryButton : MonoBehaviour
{
    public UIData uiData;
    public void ClearInvClick()
    {
        uiData.dropRequest = true;
    }
}
