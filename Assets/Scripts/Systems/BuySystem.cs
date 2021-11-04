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

                    int playerAvailableMass = playerStorage.maxMass - playerStorage.currentMass;
                    int dealMass = 0;
                    if (playerAvailableMass < seller.productsForSale)
                    {
                        dealMass = playerAvailableMass;
                    }
                    else
                    {
                        dealMass = seller.productsForSale;
                    }
                    if (dealMass != 0)
                    {
                        bool haveProduct = false;
                        foreach (var product in cargo.inventory)
                        {
                            if (product.type == seller.sellingProduct)
                            {
                                product.mass += dealMass;
                                haveProduct = true;
                            }
                        }
                        if (!haveProduct)
                        {
                            cargo.inventory.Add(new Cargo(seller.sellingProduct, dealMass));
                        }

                        seller.productsForSale -= dealMass;
                        sellerStorage.currentMass -= dealMass;
                        playerStorage.currentMass += dealMass;
                        player.playerRB.mass += dealMass;
                        seller.tradePointData.storageInfo.text = sellerStorage.currentMass + "/" + sellerStorage.maxMass;
                        sceneData.money -= dealMass * seller.sellPrice;
                        uiData.moneyText.text = sceneData.money.ToString("#");
                        Debug.Log(cargo.inventory.Count);///////////////////
                    }


                    uiData.cargoText.text = playerStorage.currentMass + "/" + playerStorage.maxMass;
                    uiData.buyRequest = false;
                }
            }
        }
    }
}
