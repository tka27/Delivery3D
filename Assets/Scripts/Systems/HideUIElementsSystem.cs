using Leopotam.Ecs;


sealed class HideUIElementsSystem : IEcsRunSystem
{
    UIData uiData;
    SceneData sceneData;


    EcsFilter<ProductSeller> sellerFilter;
    EcsFilter<ProductBuyer> buyerFilter;


    void IEcsRunSystem.Run()
    {

        if (sceneData.gameMode == GameMode.Build && !uiData.clearButton.activeSelf)
        {
            uiData.clearButton.SetActive(true);
            uiData.confirmButton.SetActive(true);
        }
        else if (sceneData.gameMode != GameMode.Build && uiData.clearButton.activeSelf)
        {
            uiData.clearButton.SetActive(false);
            uiData.confirmButton.SetActive(false);
        }

        //buyButton
        bool buyCheck = false;
        foreach (var fSeller in sellerFilter)
        {
            ref var seller = ref sellerFilter.Get1(fSeller);

            if (seller.tradePointData.ableToTrade && sceneData.gameMode == GameMode.View)
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
        foreach (var fBuyer in buyerFilter)
        {
            ref var seller = ref buyerFilter.Get1(fBuyer);

            if (seller.tradePointData.ableToTrade && sceneData.gameMode == GameMode.View)
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

        //playerInfoPanel
        /*if ((sceneData.gameMode == GameMode.View) && !uiData.playerInfoPanel[0].activeSelf)
        {
            foreach (var uiElement in uiData.playerInfoPanel)
            {
                uiElement.SetActive(true);
            }
        }
        else if (sceneData.gameMode != GameMode.View)
        {
            foreach (var uiElement in uiData.playerInfoPanel)
            {
                uiElement.SetActive(false);
            }
        }*/
    }
}
