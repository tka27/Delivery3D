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
            uiData.gameModeText.text = sceneData.gameMode.ToString();
        }
        else
        {
            sceneData.Notification("Build a road to the building");
        }
    }
}
