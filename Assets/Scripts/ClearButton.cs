using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearButton : MonoBehaviour
{
    [SerializeField] SceneData sceneData;

    public void ClearClick()
    {
        sceneData.clearPathRequest = true;
    }
}
