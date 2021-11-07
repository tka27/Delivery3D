using Leopotam.Ecs;


sealed class AutoServiceSystem : IEcsRunSystem
{
    EcsFilter<ProductSeller, StorageComp, AutoService> sellerFilter;
    EcsFilter<PlayerComp> playerFilter;
    UIData uiData;
    SceneData sceneData;
    StaticData staticData;
    void IEcsRunSystem.Run()
    {
        foreach (var fSeller in sellerFilter)
        {
            ref var seller = ref sellerFilter.Get1(fSeller);
            if (seller.tradePointData.ableToTrade && uiData.buyRequest)
            {
                uiData.buyRequest = false;

                ref var player = ref playerFilter.Get1(0);
                ref var sellerStorage = ref sellerFilter.Get2(fSeller);
                float playerAvailableMass = 0;

                if (seller.product.type == ProductType.Fuel)
                {
                    playerAvailableMass = (player.maxFuel - player.currentFuel);
                }
                else
                {
                    playerAvailableMass = (player.maxDurability - player.currentDurability);
                }
                float dealMass = 0;
                if (playerAvailableMass < seller.product.mass)
                {
                    dealMass = playerAvailableMass;
                }
                else
                {
                    dealMass = seller.product.mass;
                }
                if (dealMass * seller.currentPrice > staticData.currentMoney)
                {
                    dealMass = staticData.currentMoney / seller.currentPrice;
                }
                if (dealMass == 0)
                {
                    return;
                }

                sellerFilter.GetEntity(fSeller).Get<SellDataUpdateRequest>();
                seller.product.mass -= dealMass;
                sellerStorage.currentMass -= dealMass;
                staticData.currentMoney -= dealMass * seller.currentPrice;
                uiData.moneyText.text = staticData.currentMoney.ToString("0");
                if (seller.product.type == ProductType.Fuel)
                {
                    player.currentFuel += dealMass;
                    uiData.fuelText.text = player.currentFuel.ToString("0");
                }
                else
                {
                    player.currentDurability += dealMass;
                    uiData.durabilityText.text = player.currentDurability.ToString("0");
                }
            }
        }
    }
}