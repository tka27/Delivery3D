using Leopotam.Ecs;
using UnityEngine;
using System.Collections.Generic;


sealed class SellSystem : IEcsRunSystem
{

    EcsFilter<ProductBuyer, StorageComp> buyerFilter;
    EcsFilter<CargoComp, StorageComp, PlayerComp> playerFilter;
    UIData uiData;
    StaticData staticData;

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

                    float playerActualProductsMass = 0;
                    List<int> forRemove = new List<int>();
                    int productIndex = 0;

                    for (int i = 0; i < cargo.inventory.Count; i++)
                    {
                        if (cargo.inventory[i].type == buyer.product.type)
                        {
                            playerActualProductsMass += cargo.inventory[i].mass;
                            productIndex = i;
                        }
                    }

                    if (playerActualProductsMass == 0)
                    {
                        return;
                    }
                    var freeStorageMass = (buyerStorage.maxMass - buyerStorage.currentMass);
                    float dealMass = 0;

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
                        buyerFilter.GetEntity(fBuyer).Get<SellDataUpdateRequest>();
                        buyer.product.mass += dealMass;
                        buyerStorage.currentMass += dealMass;
                        playerStorage.currentMass -= dealMass;
                        player.playerRB.mass -= dealMass;
                        buyer.tradePointData.storageInfo.text = buyerStorage.currentMass.ToString("0") + "/" + buyerStorage.maxMass.ToString("0");
                        staticData.currentMoney += dealMass * buyer.currentPrice;
                        uiData.moneyText.text = staticData.currentMoney.ToString("0");
                        SwitchCargo();
                        buyer.tradePointData.buyCount.text = buyer.product.mass.ToString("#");
                    }
                    uiData.cargoText.text = playerStorage.currentMass.ToString("0") + "/" + playerStorage.maxMass.ToString("0");
                }
            }
        }
    }


    void SwitchCargo()
    {

        foreach (var fPlayer in playerFilter)
        {
            ref var playerStorage = ref playerFilter.Get2(fPlayer);
            ref var player = ref playerFilter.Get3(fPlayer);
            float filledPart = playerStorage.currentMass / playerStorage.maxMass;

            foreach (var go in player.playerData.playerCargo)
            {
                go.SetActive(false);
            }
            for (int i = 0; i < player.playerData.playerCargo.Count * filledPart; i++)
            {
                player.playerData.playerCargo[i].SetActive(true);
            }
        }
    }
}
