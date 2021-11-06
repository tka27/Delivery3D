using Leopotam.Ecs;
using UnityEngine;


sealed class BuySystem : IEcsRunSystem
{

    EcsFilter<ProductSeller, StorageComp> sellerFilter;
    EcsFilter<CargoComp, StorageComp, PlayerComp> playerFilter;
    UIData uiData;
    SceneData sceneData;

    void IEcsRunSystem.Run()
    {
        foreach (var fSeller in sellerFilter)
        {
            ref var seller = ref sellerFilter.Get1(fSeller);
            if (seller.tradePointData.ableToTrade && uiData.buyRequest)
            {
                ref var sellerStorage = ref sellerFilter.Get2(fSeller);
                foreach (var fCargo in playerFilter)
                {
                    ref var cargo = ref playerFilter.Get1(fCargo);
                    ref var playerStorage = ref playerFilter.Get2(fCargo);
                    ref var player = ref playerFilter.Get3(fCargo);

                    int playerAvailableMass = (int)(playerStorage.maxMass - playerStorage.currentMass);
                    int dealMass = 0;
                    if (playerAvailableMass < seller.sellingProduct.mass)
                    {
                        dealMass = playerAvailableMass;
                    }
                    else
                    {
                        dealMass = seller.sellingProduct.mass;
                    }
                    if (dealMass != 0)
                    {
                        bool haveProduct = false;
                        foreach (var product in cargo.inventory)
                        {
                            if (product.type == seller.sellingProduct.type)
                            {
                                product.mass += dealMass;
                                haveProduct = true;
                            }
                        }
                        if (!haveProduct)
                        {
                            cargo.inventory.Add(new Product(seller.sellingProduct.type, dealMass, seller.sellingProduct.icon));
                        }

                        seller.sellingProduct.mass -= dealMass;
                        sellerStorage.currentMass -= dealMass;
                        playerStorage.currentMass += dealMass;
                        player.playerRB.mass += dealMass;
                        seller.tradePointData.storageInfo.text = sellerStorage.currentMass + "/" + sellerStorage.maxMass;
                        sceneData.money -= dealMass * seller.sellPrice;
                        uiData.moneyText.text = sceneData.money.ToString("#");
                        SwitchCargo();
                        seller.tradePointData.sellCount.text = seller.sellingProduct.mass.ToString();
                    }


                    uiData.cargoText.text = playerStorage.currentMass + "/" + playerStorage.maxMass;
                    uiData.buyRequest = false;
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
