using Leopotam.Ecs;
using UnityEngine;


sealed class CargoDisplaySystem : IEcsRunSystem
{

    EcsFilter<Inventory, PlayerComp> playerStorageFilter;
    SceneData sceneData;
    void IEcsRunSystem.Run()
    {
        var player = playerStorageFilter.Get2(0);
        if (sceneData.buildCam.transform.position.y <= 10 && sceneData.gameMode == GameMode.View && !player.carData.inventoryCanvas.activeSelf)
        {
            player.carData.inventoryCanvas.SetActive(true);
            player.carData.inventoryCanvas.transform.rotation = Quaternion.identity;
        }
        else if (player.carData.inventoryCanvas.activeSelf && sceneData.buildCam.transform.position.y > 10 || sceneData.gameMode != GameMode.View)
        {
            player.carData.inventoryCanvas.SetActive(false);
        }

        var playerInventory = playerStorageFilter.Get1(0);
        if (playerInventory.GetCurrentMass() > 0 && !player.carData.clearInventoryBtn.activeSelf)
        {
            player.carData.clearInventoryBtn.SetActive(true);
        }
        else if (playerInventory.GetCurrentMass() <= 0 && player.carData.clearInventoryBtn.activeSelf)
        {
            player.carData.clearInventoryBtn.SetActive(false);
        }
    }
}
