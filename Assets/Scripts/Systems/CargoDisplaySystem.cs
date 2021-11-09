using Leopotam.Ecs;
using UnityEngine;


sealed class CargoDisplaySystem : IEcsRunSystem
{

    EcsFilter<PlayerComp> playerFilter;
    EcsFilter<StorageComp, PlayerComp> playerStorageFilter;
    SceneData sceneData;
    void IEcsRunSystem.Run()
    {
        var player = playerFilter.Get1(0);
        if (sceneData.buildCam.transform.position.y <= 10 && sceneData.gameMode == GameMode.View && !player.carData.inventoryCanvas.activeSelf)
        {
            player.carData.inventoryCanvas.SetActive(true);
            player.carData.inventoryCanvas.transform.rotation = Quaternion.identity;
        }
        else if (player.carData.inventoryCanvas.activeSelf && sceneData.buildCam.transform.position.y > 10 || sceneData.gameMode != GameMode.View)
        {
            player.carData.inventoryCanvas.SetActive(false);
        }

        var storage = playerStorageFilter.Get1(0);
        if (storage.currentMass > 0 && !player.carData.clearInventoryBtn.activeSelf)
        {
            player.carData.clearInventoryBtn.SetActive(true);
        }
        else if (storage.currentMass <= 0 && player.carData.clearInventoryBtn.activeSelf)
        {
            player.carData.clearInventoryBtn.SetActive(false);
        }
    }
}
