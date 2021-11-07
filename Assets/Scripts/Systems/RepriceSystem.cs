using Leopotam.Ecs;
using UnityEngine;


sealed class RepriceSystem : IEcsRunSystem
{
    EcsFilter<StorageComp, ProductSeller, SellDataUpdateRequest> sellerFilter;
    EcsFilter<StorageComp, ProductBuyer, BuyDataUpdateRequest> buyerFilter;

    void IEcsRunSystem.Run()
    {
        foreach (var fSeller in sellerFilter)
        {
            sellerFilter.GetEntity(fSeller).Del<SellDataUpdateRequest>();
            ref var storage = ref sellerFilter.Get1(fSeller);
            ref var seller = ref sellerFilter.Get2(fSeller);
            if (seller.product.mass / storage.maxMass > 0.8f)
            {
                seller.currentPrice = seller.product.defaultPrice / seller.repriceMultiplier;
            }
            else
            {
                seller.currentPrice = seller.product.defaultPrice;
            }
            seller.tradePointData.sellCount.text = seller.product.mass.ToString("0");
            seller.tradePointData.storageInfo.text = storage.currentMass.ToString("0") + "/" + storage.maxMass.ToString("0");
            seller.tradePointData.sellPrice.text = seller.currentPrice.ToString("0.00");
        }
        foreach (var fBuyer in buyerFilter)
        {
            buyerFilter.GetEntity(fBuyer).Del<BuyDataUpdateRequest>();
            ref var storage = ref buyerFilter.Get1(fBuyer);
            ref var buyer = ref buyerFilter.Get2(fBuyer);
            if (storage.currentMass / storage.maxMass > 0.5f)
            {
                buyer.currentPrice = buyer.product.defaultPrice / buyer.repriceMultiplier;
            }
            else
            {
                buyer.currentPrice = buyer.product.defaultPrice;
            }
            buyer.tradePointData.buyCount.text = buyer.product.mass.ToString("0");
            buyer.tradePointData.storageInfo.text = storage.currentMass.ToString("0") + "/" + storage.maxMass.ToString("0");
            buyer.tradePointData.buyPrice.text = buyer.currentPrice.ToString("0.00");
        }
    }
}
