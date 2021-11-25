using Leopotam.Ecs;
using UnityEngine;


sealed class FuelSystem : IEcsRunSystem
{

    EcsFilter<Player, MovableComp> playerFilter;
    SceneData sceneData;
    UIData uiData;

    void IEcsRunSystem.Run()
    {
        if (sceneData.gameMode == GameMode.Drive && Input.GetMouseButton(0))
        {
            foreach (var fPlayer in playerFilter)
            {
                ref var player = ref playerFilter.Get1(fPlayer);
                player.currentFuel -= player.carData.acceleration / 2000;
                uiData.fuelText.text = player.currentFuel.ToString("#");
                if (player.currentFuel < 0)
                {
                    player.currentFuel = 0;
                    sceneData.Notification("No Fuel");
                    playerFilter.GetEntity(fPlayer).Get<ImmobilizeRequest>();
                }
            }
        }
    }
}
