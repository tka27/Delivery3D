using Leopotam.Ecs;


sealed class InventoryDisplaySystem : IEcsRunSystem
{

    SceneData sceneData;
    UIData uiData;
    void IEcsRunSystem.Run()
    {

        if (sceneData.buildCam.transform.position.y <= 10 && sceneData.gameMode == GameMode.View && !uiData.inventoryCanvas.activeSelf)
        {
            uiData.inventoryCanvas.SetActive(true);
        }
        else if (uiData.inventoryCanvas.activeSelf && sceneData.buildCam.transform.position.y > 10 || sceneData.gameMode != GameMode.View)
        {
            uiData.inventoryCanvas.SetActive(false);
        }

    }
}
