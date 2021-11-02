using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmButton : MonoBehaviour
{
    [SerializeField] SceneData sceneData;
    public void ClickConfirm()
    {
        if (sceneData.isPathComplete)
        {
            sceneData.isPathConfirmed = true;
            sceneData.gameMode = GameMode.Drive;
            sceneData.gameModeText.text = sceneData.gameMode.ToString();
        }
        else
        {
            Debug.Log("Проложи путь полностью");
        }
    }

}