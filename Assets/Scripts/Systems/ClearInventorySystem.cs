using Leopotam.Ecs;
using UnityEngine;

sealed class ClearInventorySystem : IEcsRunSystem
{
    EcsFilter<PlayerComp, Inventory>.Exclude<UpdateCargoRequest> playerFilter;
    UIData uiData;
    void IEcsRunSystem.Run()
    {
        foreach (var fPlayer in playerFilter)
        {
            if (!uiData.dropRequest)
            {
                return;
            }
            uiData.dropRequest = false;
            var player = playerFilter.Get1(fPlayer);
            var cargo = playerFilter.Get2(fPlayer);
            player.playerRB.AddRelativeForce(new Vector3(0, -100000, 100000));
            for (int i = 0; i < player.carData.playerCargo.Count; i++)
            {


                player.carData.playerCargoRB[i].isKinematic = false;
                player.carData.playerCargoRB[i].AddExplosionForce(Random.Range(2000, 3000), player.carData.centerOfMass.transform.position, 0);
            }
            cargo.inventory.Clear();
            playerFilter.GetEntity(fPlayer).Get<UpdateCargoRequest>();
        }
    }
}