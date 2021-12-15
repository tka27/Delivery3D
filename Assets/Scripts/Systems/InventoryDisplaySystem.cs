using Leopotam.Ecs;


sealed class InventoryDisplaySystem : IEcsRunSystem
{

    SceneData sceneData;
    BuildingsData buildingsData;
    UIData uiData;
    const float CAM_Y_POS = 13;
    void IEcsRunSystem.Run()
    {

        if (sceneData.buildCam.position.y <= CAM_Y_POS && sceneData.gameMode == GameMode.View && !uiData.inventoryCanvas.activeSelf)
        {
            uiData.inventoryCanvas.SetActive(true);
            foreach (var panel in buildingsData.tradePointCanvases)
            {
                panel.SetActive(false);
            }

        }
        else if (uiData.inventoryCanvas.activeSelf && sceneData.buildCam.position.y > CAM_Y_POS || sceneData.gameMode != GameMode.View)
        {
            uiData.inventoryCanvas.SetActive(false);
            foreach (var panel in buildingsData.tradePointCanvases)
            {
                panel.SetActive(true);
            }
        }
    }
}
