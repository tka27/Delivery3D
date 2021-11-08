using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;


sealed class ShopQuestSystem : IEcsRunSystem
{

    EcsFilter<ProductBuyer, Quest> shopFilter;
    EcsFilter<ProductSeller>.Exclude<AutoService> sellerFilter;

    void IEcsRunSystem.Run()
    {
        foreach (var fShop in shopFilter)
        {
            ref var quest = ref shopFilter.Get2(fShop);

            quest.timer--;
            if (quest.timer > 0)
            {
                continue;
            }
            quest.timer = 50;

            ref var buyer = ref shopFilter.Get1(fShop);
            quest.currentQuestTime--;
            buyer.tradePointData.currentQuestTime.text = quest.currentQuestTime.ToString("0");
            if (quest.currentQuestTime > 0)
            {
                continue;
            }
            quest.currentQuestTime = quest.maxQuestTime;
            Product product = SelectRandomProducedProduct();
            buyer.product = new Product(product.type, product.icon, product.defaultPrice * 2f);
            buyer.tradePointData.buyProductSpriteRenderer.sprite = buyer.product.icon;
            buyer.tradePointData.buyProductSpriteRendererCopy.sprite = buyer.product.icon;
            shopFilter.GetEntity(fShop).Get<BuyDataUpdateRequest>();
        }
    }

    Product SelectRandomProducedProduct()
    {
        List<Product> products = new List<Product>();
        foreach (var fSeller in sellerFilter)
        {
            ref var seller = ref sellerFilter.Get1(fSeller);
            products.Add(seller.product);
        }
        int randomIndex = Random.Range(0, products.Count);
        if (products.Count > 0)
        {
            return products[randomIndex];
        }
        return null;
    }
}
