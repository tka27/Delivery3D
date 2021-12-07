using Leopotam.Ecs;


sealed class InfoPanelSwitchSystem : IEcsRunSystem
{
    SceneData sceneData;
    BuildingsData buildingsData;
    void IEcsRunSystem.Run()
    {
        if (sceneData.gameMode != GameMode.Drive && sceneData.buildCam.transform.position.y > 10 && !buildingsData.tradePointCanvases[0].activeSelf)
        {
            foreach (var panel in buildingsData.tradePointCanvases)
            {
                panel.SetActive(true);
            }
        }
        else if (sceneData.gameMode == GameMode.Drive && buildingsData.tradePointCanvases[0].activeSelf || sceneData.buildCam.transform.position.y <= 10 && buildingsData.tradePointCanvases[0].activeSelf)
        {
            foreach (var panel in buildingsData.tradePointCanvases)
            {
                panel.SetActive(false);
            }
        }
    }
}