using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmButton : MonoBehaviour
{
    [SerializeField] UIData uiData;
    [SerializeField] SceneData sceneData;
    public void ConfirmClick()
    {
        if (uiData.isPathComplete)
        {
            uiData.isPathConfirmed = true;
            sceneData.gameMode = GameMode.Drive;
            sceneData.gameModeText.text = sceneData.gameMode.ToString();
        }
        else
        {
            Debug.Log("Проложи путь полностью");
        }
    }
}
