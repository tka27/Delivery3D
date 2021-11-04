using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearButton : MonoBehaviour
{
    [SerializeField]UIData uiData;
    public void ClearClick()
    {
        uiData.clearPathRequest = true;
    }
}
