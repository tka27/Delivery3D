using Leopotam.Ecs;
using UnityEngine;
using System.Collections.Generic;


sealed class SellSystem : IEcsRunSystem
{

    EcsFilter<ProductBuyer, StorageComp> buyerFilter;
    EcsFilter<CargoComp, StorageComp, PlayerComp> playerFilter;
    UIData uiData;
    SceneData sceneData;

    void IEcsRunSystem.Run()
    {
        foreach (var fBuyer in buyerFilter)
        {
            ref var buyer = ref buyerFilter.Get1(fBuyer);
            if (buyer.tradePointData.ableToTrade && uiData.sellRequest)
            {
                uiData.sellRequest = false;
                ref var buyerStorage = ref buyerFilter.Get2(fBuyer);
                foreach (var fCargo in playerFilter)
                {
                    ref var cargo = ref playerFilter.Get1(fCargo);
                    ref var playerStorage = ref playerFilter.Get2(fCargo);
                    ref var player = ref playerFilter.Get3(fCargo);

                    int playerActualProductsMass = 0;
                    List<int> forRemove = new List<int>();
                    int productIndex = 0;

                    for (int i = 0; i < cargo.inventory.Count; i++)
                    {
                        if (cargo.inventory[i].type == buyer.buyingProduct)
                        {
                            playerActualProductsMass += cargo.inventory[i].mass;
                            productIndex = i;
                        }
                    }
                   
                    if (playerActualProductsMass == 0)
                    {
                        return;
                    }
                    var freeStorageMass = buyerStorage.maxMass - buyerStorage.currentMass;
                    int dealMass = 0;

                    if (freeStorageMass < playerActualProductsMass)
                    {
                        dealMass = freeStorageMass;
                    }
                    else
                    {
                        dealMass = playerActualProductsMass;
                    }

                    if (dealMass != 0)
                    {
                        cargo.inventory[productIndex].mass -= dealMass;
                        if (cargo.inventory[productIndex].mass == 0)
                        {
                            cargo.inventory.RemoveAt(productIndex);
                        }
                        buyer.consumableProductsCount += dealMass;
                        buyerStorage.currentMass += dealMass;
                        playerStorage.currentMass -= dealMass;
                        player.playerRB.mass -= dealMass;
                        buyer.tradePointData.storageInfo.text = buyerStorage.currentMass + "/" + buyerStorage.maxMass;
                        sceneData.money += dealMass * buyer.buyPrice;
                        uiData.moneyText.text = sceneData.money.ToString("#");
                    }

                    uiData.cargoText.text = playerStorage.currentMass + "/" + playerStorage.maxMass;
                }
            }
        }
    }
}
