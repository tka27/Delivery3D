using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeButton : MonoBehaviour
{
    [SerializeField] SceneData sceneData;

    public void OnClick()
    {
        switch (sceneData.gameMode)
        {
            case GameMode.View:
                if (!sceneData.isPathConfirmed)
                {
                    sceneData.gameMode = GameMode.Build;
                }
                else
                {
                    sceneData.gameMode = GameMode.Drive;
                }
                break;

            case GameMode.Build:
                if (sceneData.isPathConfirmed)
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
        sceneData.gameModeText.text = sceneData.gameMode.ToString();
    }
}
