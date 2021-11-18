using Leopotam.Ecs;
using UnityEngine;


sealed class UpdateCargoSystem : IEcsRunSystem
{

    EcsFilter<Inventory, PlayerComp, UpdateCargoRequest> playerFilter;
    StaticData staticData;
    UIData uiData;
    void IEcsRunSystem.Run()
    {
        foreach (var fPlayer in playerFilter)
        {
            ref var playerInventory = ref playerFilter.Get1(fPlayer);
            ref var player = ref playerFilter.Get2(fPlayer);
            player.playerRB.mass = player.carData.defaultMass + playerInventory.GetCurrentMass();
            uiData.cargoText.text = playerInventory.GetCurrentMass().ToString("0") + "/" + playerInventory.maxMass.ToString("0");
            uiData.moneyText.text = staticData.currentMoney.ToString("0.0");
            playerFilter.GetEntity(fPlayer).Del<UpdateCargoRequest>();
            playerFilter.GetEntity(fPlayer).Get<CratesDisplayRequest>();
        }
    }
}
