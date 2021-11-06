using Leopotam.Ecs;


sealed class AutoServiceSystem : IEcsRunSystem
{
    EcsFilter<ProductSeller, StorageComp, AutoService> sellerFilter;
    EcsFilter<PlayerComp> playerFilter;
    UIData uiData;
    SceneData sceneData;
    void IEcsRunSystem.Run()
    {
        foreach (var fSeller in sellerFilter)
        {
            ref var seller = ref sellerFilter.Get1(fSeller);
            if (seller.tradePointData.ableToTrade && uiData.buyRequest)
            {
                ref var player = ref playerFilter.Get1(0);
                ref var sellerStorage = ref sellerFilter.Get2(fSeller);
                float playerAvailableMass = 0;
                if (seller.sellingProduct.type == ProductType.Fuel)
                {
                    playerAvailableMass = (player.maxFuel - player.currentFuel);
                }
                else
                {
                    playerAvailableMass = (player.maxDurability - player.currentDurability);
                }
                float dealMass = 0;
                if (playerAvailableMass < seller.sellingProduct.mass)
                {
                    dealMass = playerAvailableMass;
                }
                else
                {
                    dealMass = seller.sellingProduct.mass;
                }
                if (dealMass * seller.sellPrice > sceneData.money)
                {
                    dealMass = sceneData.money / seller.sellPrice;
                }
                if (dealMass == 0)
                {
                    return;
                }

                seller.sellingProduct.mass -= dealMass;
                sellerStorage.currentMass -= dealMass;
                seller.tradePointData.storageInfo.text = sellerStorage.currentMass.ToString("0") + "/" + sellerStorage.maxMass.ToString("0");
                sceneData.money -= dealMass * seller.sellPrice;
                uiData.moneyText.text = sceneData.money.ToString("0");
                seller.tradePointData.sellCount.text = seller.sellingProduct.mass.ToString("0");
                if (seller.sellingProduct.type == ProductType.Fuel)
                {
                    player.currentFuel += dealMass;
                    uiData.fuelText.text = player.currentFuel.ToString("0");
                }
                else
                {
                    player.currentDurability += dealMass;
                    uiData.durabilityText.text = player.currentDurability.ToString("0");
                }

                uiData.buyRequest = false;
            }
        }
    }
}