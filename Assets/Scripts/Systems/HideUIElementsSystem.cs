using Leopotam.Ecs;
using UnityEngine;


sealed class HideUIElementsSystem : IEcsInitSystem
{
    UIData uiData;
    SceneData sceneData;
    PathData pathData;


    EcsFilter<ProductSeller> sellerFilter;
    EcsFilter<ProductBuyer> buyerFilter;

    public void Init()
    {
        TradePointData.tradeEvent += SwitchTradeBtns;
        UIData.updateUIEvent += UpdateUI;
    }


    void SwitchTradeBtns()
    {
        //buyButton
        bool buyCheck = false;
        foreach (var entitySeller in sellerFilter)
        {
            ref var seller = ref sellerFilter.Get1(entitySeller);

            if (seller.tradePointData.ableToTrade)
            {
                if (!uiData.buyButton.activeSelf)
                {
                    uiData.buyButton.SetActive(true);
                }
                buyCheck = true;
                break;
            }
        }
        if (!buyCheck && uiData.buyButton.activeSelf)
        {
            uiData.buyButton.SetActive(false);
        }

        //sellButton
        bool sellCheck = false;
        foreach (var entityBuyer in buyerFilter)
        {
            ref var seller = ref buyerFilter.Get1(entityBuyer);

            if (seller.tradePointData.ableToTrade)
            {
                if (!uiData.sellButton.activeSelf)
                {
                    uiData.sellButton.SetActive(true);
                }
                sellCheck = true;
                break;
            }
        }
        if (!sellCheck && uiData.sellButton.activeSelf)
        {
            uiData.sellButton.SetActive(false);
        }
    }

    void UpdateUI()
    {
        if (sceneData.gameMode == GameMode.Build)
        {
            uiData.buildBtns.SetActive(true);
            pathData.buildSphere.gameObject.SetActive(true);
        }
        else
        {
            uiData.buildBtns.SetActive(false);
            pathData.buildSphere.gameObject.SetActive(false);
        }

        if (sceneData.gameMode == GameMode.View)
        {
            uiData.tradeBtns.SetActive(true);
        }
        else
        {
            uiData.tradeBtns.SetActive(false);
        }
        uiData.gameModeText.text = sceneData.gameMode.ToString();
    }
}
