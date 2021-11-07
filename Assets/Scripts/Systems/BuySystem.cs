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
                foreach (var fCargo in playerFilter)
                {
                    ref var cargo = ref playerFilter.Get1(fCargo);
                    ref var playerStorage = ref playerFilter.Get2(fCargo);
                    ref var player = ref playerFilter.Get3(fCargo);

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
                    seller.product.mass -= dealMass;
                    sellerStorage.currentMass -= dealMass;
                    playerStorage.currentMass += dealMass;
                    player.playerRB.mass += dealMass;
                    seller.tradePointData.storageInfo.text = sellerStorage.currentMass.ToString("0") + "/" + sellerStorage.maxMass.ToString("0");
                    staticData.currentMoney -= dealMass * seller.currentPrice;
                    uiData.moneyText.text = staticData.currentMoney.ToString("0");
                    SwitchCargo();
                    seller.tradePointData.sellCount.text = seller.product.mass.ToString("0");
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
