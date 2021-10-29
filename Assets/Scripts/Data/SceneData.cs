using UnityEngine;
using UnityEngine.UI;
public class SceneData : MonoBehaviour
{
    public GameObject car;
    public GameMode gameMode;
    public Button buildMode;
}

public enum GameMode { Look, Build, Drive }

