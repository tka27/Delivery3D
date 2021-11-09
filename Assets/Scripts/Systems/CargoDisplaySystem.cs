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
        if (sceneData.buildCam.transform.position.y <= 10 && sceneData.gameMode == GameMode.View && !player.playerData.inventoryCanvas.activeSelf)
        {
            player.playerData.inventoryCanvas.SetActive(true);
            player.playerData.inventoryCanvas.transform.rotation = Quaternion.identity;
        }
        else if (player.playerData.inventoryCanvas.activeSelf && sceneData.buildCam.transform.position.y > 10 || sceneData.gameMode != GameMode.View)
        {
            player.playerData.inventoryCanvas.SetActive(false);
        }

        var storage = playerStorageFilter.Get1(0);
        if (storage.currentMass > 0 && !player.playerData.clearInventoryBtn.activeSelf)
        {
            player.playerData.clearInventoryBtn.SetActive(true);
        }
        else if (storage.currentMass <= 0 && player.playerData.clearInventoryBtn.activeSelf)
        {
            player.playerData.clearInventoryBtn.SetActive(false);
        }
    }
}
