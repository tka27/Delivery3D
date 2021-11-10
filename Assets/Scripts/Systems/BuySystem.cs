using Leopotam.Ecs;
using UnityEngine;


sealed class BuySystem : IEcsRunSystem
{

    EcsFilter<ProductSeller, Inventory>.Exclude<AutoService> sellerFilter;
    EcsFilter<Inventory, PlayerComp> playerFilter;
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
                    ref var playerInventory = ref playerFilter.Get1(fPlayer);
                    ref var player = ref playerFilter.Get2(fPlayer);

                    float playerAvailableMass = (playerInventory.maxMass - playerInventory.GetCurrentMass());
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
                    if (dealMass * seller.product.currentPrice > staticData.currentMoney)
                    {
                        dealMass = staticData.currentMoney / seller.product.currentPrice;
                    }
                    bool haveProduct = false;
                    foreach (var product in playerInventory.inventory)
                    {
                        if (product.type == seller.product.type)
                        {
                            product.mass += dealMass;
                            haveProduct = true;
                        }
                    }
                    if (!haveProduct)
                    {
                        playerInventory.inventory.Add(new Product(seller.product.type, dealMass, seller.product.icon, seller.product.defaultPrice));
                    }
                    sellerFilter.GetEntity(fSeller).Get<SellDataUpdateRequest>();
                    playerFilter.GetEntity(0).Get<UpdateCargoRequest>();

                    seller.product.mass -= dealMass;
                    staticData.currentMoney -= dealMass * seller.product.currentPrice;
                 
                    foreach (var go in player.carData.playerCargo)
                    {
                        go.SetActive(false);
                    }
                }
            }
        }
    }
}
