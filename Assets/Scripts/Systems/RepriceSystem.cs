using Leopotam.Ecs;
using UnityEngine;


sealed class RepriceSystem : IEcsRunSystem
{
    EcsFilter<Inventory, ProductSeller, SellDataUpdateRequest> sellerFilter;
    EcsFilter<Inventory, ProductBuyer, BuyDataUpdateRequest> buyerFilter;

    void IEcsRunSystem.Run()
    {
        foreach (var fSeller in sellerFilter)
        {
            sellerFilter.GetEntity(fSeller).Del<SellDataUpdateRequest>();
            ref var sellerInventory = ref sellerFilter.Get1(fSeller);
            ref var seller = ref sellerFilter.Get2(fSeller);
            if (seller.product.mass / sellerInventory.maxMass > 0.8f)//fix
            {
                seller.product.currentPrice = seller.product.defaultPrice / seller.repriceMultiplier;
            }
            else
            {
                seller.product.currentPrice = seller.product.defaultPrice;
            }
            seller.tradePointData.sellCount.text = seller.product.mass.ToString("0");
            seller.tradePointData.storageInfo.text = sellerInventory.GetCurrentMass().ToString("0") + "/" + sellerInventory.maxMass.ToString("0");
            seller.tradePointData.sellPrice.text = seller.product.currentPrice.ToString("0.00");
        }
        foreach (var fBuyer in buyerFilter)
        {
            buyerFilter.GetEntity(fBuyer).Del<BuyDataUpdateRequest>();
            ref var buyerInventory = ref buyerFilter.Get1(fBuyer);
            ref var buyer = ref buyerFilter.Get2(fBuyer);

            foreach (var product in buyer.buyingProductTypes)
            {
                for (int i = 0; i < buyerInventory.inventory.Count; i++)
                {
                    Product inventoryItem = buyerInventory.inventory[i];
                    if (product == inventoryItem.type)
                    {
                        if (inventoryItem.mass > buyerInventory.maxMass / (buyer.buyingProductTypes.Count * 1.5f))
                        {
                            inventoryItem.currentPrice = inventoryItem.defaultPrice * 0.5f;
                        }
                        else
                        {
                            inventoryItem.currentPrice = inventoryItem.defaultPrice;
                        }


                        buyer.tradePointData.buyCount.text = inventoryItem.mass.ToString("0");//fixed
                        buyer.tradePointData.buyPrice.text = inventoryItem.currentPrice.ToString("0.00");//fixed
                    }
                }
            }


            buyer.tradePointData.storageInfo.text = buyerInventory.GetCurrentMass().ToString("0") + "/" + buyerInventory.maxMass.ToString("0");
        }
    }
}
