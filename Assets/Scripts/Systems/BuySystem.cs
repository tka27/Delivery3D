using Leopotam.Ecs;
using UnityEngine;


sealed class BuySystem : IEcsRunSystem
{

    EcsFilter<ProductSeller, StorageComp> sellerFilter;
    EcsFilter<CargoComp, StorageComp> cargoFilter;
    UIData uiData;

    void IEcsRunSystem.Run()
    {
        foreach (var fSeller in sellerFilter)
        {
            ref var seller = ref sellerFilter.Get1(fSeller);
            ref var sellerStorage = ref sellerFilter.Get2(fSeller);
            if (seller.tradePointData.ableToTrade && uiData.buyRequest)
            {
                foreach (var fCargo in cargoFilter)
                {
                    ref var cargo = ref cargoFilter.Get1(fCargo);
                    ref var playerStorage = ref cargoFilter.Get2(fCargo);

                    int playerAvailableMass = playerStorage.maxMass - playerStorage.currentMass;
                    if (playerAvailableMass < sellerStorage.currentMass)
                    {
                        cargo.inventory.Add(new Cargo(seller.sellingProduct, playerAvailableMass));
                        sellerStorage.currentMass -= playerAvailableMass;
                        playerStorage.currentMass += playerAvailableMass;
                    }
                    else
                    {
                        cargo.inventory.Add(new Cargo(seller.sellingProduct, sellerStorage.currentMass));
                        playerStorage.currentMass += sellerStorage.currentMass;
                        sellerStorage.currentMass = 0;
                    }
                    uiData.cargoText.text = playerStorage.currentMass + "/" + playerStorage.maxMass;
                    uiData.buyRequest = false;
                }
            }
        }
    }
}
