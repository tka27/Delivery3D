using Leopotam.Ecs;
using UnityEngine;


sealed class FuelSystem : IEcsRunSystem
{

    EcsFilter<PlayerComp, MovableComp> playerFilter;
    SceneData sceneData;

    void IEcsRunSystem.Run()
    {
        if (sceneData.gameMode == GameMode.Drive && Input.GetMouseButton(0))
        {
            foreach (var fPlayer in playerFilter)
            {
                ref var player = ref playerFilter.Get1(fPlayer);
                player.currentFuel -= player.fuelConsumption;
                Debug.Log(player.currentFuel);
                if (player.currentFuel < 0)
                {
                    player.currentFuel = 0;
                    playerFilter.GetEntity(fPlayer).Get<ImmobilizeRequest>();
                }
            }
        }
    }
}
