using Leopotam.Ecs;
using UnityEngine;


sealed class CargoDropSystem : IEcsRunSystem
{
    EcsFilter<PlayerComp, CargoComp> playerFilter;
    UIData uiData;
    void IEcsRunSystem.Run()
    {
        if (!uiData.dropRequest)
        {
            return;
        }
        uiData.dropRequest = false;
        var player = playerFilter.Get1(0);
        var cargo = playerFilter.Get2(0);
        player.playerRB.AddRelativeForce(new Vector3(0, -100000, 100000));
        for (int i = 0; i < player.carData.playerCargo.Count; i++)
        {


            player.carData.playerCargoRB[i].isKinematic = false;
            player.carData.playerCargoRB[i].AddExplosionForce(Random.Range(2000, 3000), player.carData.centerOfMass.transform.position, 0);
        }
        cargo.inventory.Clear();
        playerFilter.GetEntity(0).Get<UpdateCargoRequest>();
    }
}
