using Leopotam.Ecs;


sealed class HideButtonsSystem : IEcsRunSystem
{
    SceneData sceneData;
    

    void IEcsRunSystem.Run()
    {
        sceneData.clearButton.SetActive(sceneData.gameMode == GameMode.Build);
        sceneData.confirmButton.SetActive(sceneData.gameMode == GameMode.Build);
    }
}
