using Leopotam.Ecs;
using UnityEngine;


sealed class UpdateCargoSystem : IEcsRunSystem
{

    EcsFilter<StorageComp, CargoComp, PlayerComp, UpdateCargoRequest> playerFilter;
    StaticData staticData;
    UIData uiData;
    void IEcsRunSystem.Run()
    {
        foreach (var fPlayer in playerFilter)
        {
            ref var playerStorage = ref playerFilter.Get1(fPlayer);
            ref var cargo = ref playerFilter.Get2(fPlayer);
            ref var player = ref playerFilter.Get3(fPlayer);
            float cargoMass = 0;
            foreach (var prod in cargo.inventory)
            {
                cargoMass += prod.mass;
            }
            player.playerRB.mass = player.defaultRBMass + cargoMass;
            playerStorage.currentMass = cargoMass;
            uiData.cargoText.text = playerStorage.currentMass.ToString("0") + "/" + playerStorage.maxMass.ToString("0");
            uiData.moneyText.text = staticData.currentMoney.ToString("0.0");
            playerFilter.GetEntity(fPlayer).Del<UpdateCargoRequest>();
            playerFilter.GetEntity(fPlayer).Get<CratesDisplayRequest>();
        }
    }
}
