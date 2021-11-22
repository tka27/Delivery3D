using Leopotam.Ecs;
using UnityEngine;


sealed class CargoDisplaySystem : IEcsRunSystem
{

    EcsFilter<Inventory, PlayerComp> playerStorageFilter;
    SceneData sceneData;
    UIData uiData;
    void IEcsRunSystem.Run()
    {

        if (sceneData.buildCam.transform.position.y <= 10 && sceneData.gameMode == GameMode.View && !uiData.inventoryCanvas.activeSelf)
        {
            var player = playerStorageFilter.Get2(0);
            var playerInventory = playerStorageFilter.Get1(0);
            uiData.inventoryCanvas.SetActive(true);
        }
        else if (uiData.inventoryCanvas.activeSelf && sceneData.buildCam.transform.position.y > 10 || sceneData.gameMode != GameMode.View)
        {
            uiData.inventoryCanvas.SetActive(false);
        }

    }
}
