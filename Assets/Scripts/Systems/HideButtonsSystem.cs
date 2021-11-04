using Leopotam.Ecs;


sealed class HideButtonsSystem : IEcsRunSystem
{
    UIData uiData;
    SceneData sceneData;


    void IEcsRunSystem.Run()
    {
        if (sceneData.gameMode == GameMode.Build && !uiData.clearButton.activeSelf)
        {
            uiData.clearButton.SetActive(true);
            uiData.confirmButton.SetActive(true);
        }
        else if (sceneData.gameMode != GameMode.Build && uiData.clearButton.activeSelf)
        {
            uiData.clearButton.SetActive(false);
            uiData.confirmButton.SetActive(false);
        }

    }
}
