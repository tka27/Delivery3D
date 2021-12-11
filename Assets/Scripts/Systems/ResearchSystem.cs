using Leopotam.Ecs;


sealed class ResearchSystem : IEcsRunSystem
{
    StaticData staticData;
    SceneData sceneData;

    EcsFilter<ResearchLab, Inventory, ProductBuyer> labFilter;
    float timer;



    void IEcsRunSystem.Run()
    {
        if (timer > 0)
        {
            timer--;
            return;
        }
        timer = 50;

        foreach (var labEntity in labFilter)
        {
            ref var lab = ref labFilter.Get1(labEntity);
            ref var labInventory = ref labFilter.Get2(labEntity);
            ref var labBuyer = ref labFilter.Get3(labEntity);


            bool haveProduct = false;
            foreach (var inventoryItem in labInventory.inventory)
            {
                if (inventoryItem.type == labBuyer.buyingProductTypes[0])
                {
                    haveProduct = true;
                }
            }
            if (!haveProduct) return;

            if (labInventory.inventory[0].mass > 0)
            {
                labInventory.inventory[0].mass--;
                lab.progress++;
            }
            else return;



            if (lab.progress >= lab.requirement)
            {
                staticData.researchLvl++;
                if (staticData.researchLvl >= sceneData.researchList.Length)
                {
                    labFilter.GetEntity(labEntity).Del<ResearchLab>();
                    labBuyer.tradePointData.labProgress.text = "Max lvl";
                    return;
                }
                labFilter.GetEntity(labEntity).Get<LabUpdateRequest>();

            }

            labBuyer.tradePointData.labProgress.text = lab.progress.ToString() + "/" + lab.requirement.ToString();
            labFilter.GetEntity(labEntity).Get<BuyDataUpdateRequest>();
        }
    }

    void DispayReward()
    {
        switch (staticData.researchLvl)
        {
            case 3:
                staticData.carsUnlockStatus[3] = true;
                sceneData.Notification("Unlocked new car");
                break;

            default: return;
        }



    }

}
