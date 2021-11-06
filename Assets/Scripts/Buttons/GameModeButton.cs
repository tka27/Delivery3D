using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeButton : MonoBehaviour
{
    [SerializeField] SceneData sceneData;
    [SerializeField] UIData uiData;
    public void GameModeClick()
    {
        switch (sceneData.gameMode)
        {
            case GameMode.View:
                if (!uiData.isPathConfirmed)
                {
                    sceneData.gameMode = GameMode.Build;
                }
                else
                {
                    sceneData.gameMode = GameMode.Drive;
                }
                break;

            case GameMode.Build:
                if (uiData.isPathConfirmed)
                {
                    sceneData.gameMode = GameMode.Drive;
                }
                else
                {
                    sceneData.gameMode = GameMode.View;
                }
                break;

            default:
                sceneData.gameMode = GameMode.View;
                break;
        }
        uiData.gameModeText.text = sceneData.gameMode.ToString();
    }
}
