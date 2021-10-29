using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeButton : MonoBehaviour
{
    public Text buttonText;
    [SerializeField] SceneData sceneData;

    public void OnClick()
    {
        switch (sceneData.gameMode)
        {
            case GameMode.Look:
                sceneData.gameMode = GameMode.Build;
                break;

            case GameMode.Build:
                sceneData.gameMode = GameMode.Drive;
                break;

            default:
                sceneData.gameMode = GameMode.Look;
                break;
        }
        buttonText.text = sceneData.gameMode.ToString();
    }
}
