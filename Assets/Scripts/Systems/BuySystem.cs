using Leopotam.Ecs;
using UnityEngine;


sealed class BuySystem : IEcsRunSystem
{

    EcsFilter<ProductSeller, StorageComp>.Exclude<AutoService> sellerFilter;
    EcsFilter<CargoComp, StorageComp, PlayerComp> playerFilter;
    UIData uiData;
    StaticData staticData;

    void IEcsRunSystem.Run()
    {
        foreach (var fSeller in sellerFilter)
        {
            ref var seller = ref sellerFilter.Get1(fSeller);
            if (seller.tradePointData.ableToTrade && uiData.buyRequest)
            {
                uiData.buyRequest = false;
                ref var sellerStorage = ref sellerFilter.Get2(fSeller);
                foreach (var fPlayer in playerFilter)
                {
                    ref var cargo = ref playerFilter.Get1(fPlayer);
                    ref var playerStorage = ref playerFilter.Get2(fPlayer);
                    ref var player = ref playerFilter.Get3(fPlayer);

                    float playerAvailableMass = (playerStorage.maxMass - playerStorage.currentMass);
                    float dealMass = 0;
                    if (playerAvailableMass < seller.product.mass)
                    {
                        dealMass = playerAvailableMass;
                    }
                    else
                    {
                        dealMass = seller.product.mass;
                    }
                    if (dealMass == 0)
                    {
                        return;
                    }
                    if (dealMass * seller.currentPrice > staticData.currentMoney)
                    {
                        dealMass = staticData.currentMoney / seller.currentPrice;
                    }
                    bool haveProduct = false;
                    foreach (var product in cargo.inventory)
                    {
                        if (product.type == seller.product.type)
                        {
                            product.mass += dealMass;
                            haveProduct = true;
                        }
                    }
                    if (!haveProduct)
                    {
                        cargo.inventory.Add(new Product(seller.product.type, dealMass, seller.product.icon, seller.product.defaultPrice));
                    }
                    sellerFilter.GetEntity(fSeller).Get<SellDataUpdateRequest>();
                    playerFilter.GetEntity(0).Get<UpdateCargoRequest>();

                    seller.product.mass -= dealMass;
                    sellerStorage.currentMass -= dealMass;
                    staticData.currentMoney -= dealMass * seller.currentPrice;
                 
                    foreach (var go in player.playerData.playerCargo)
                    {
                        go.SetActive(false);
                    }
                }
            }
        }
    }
}
