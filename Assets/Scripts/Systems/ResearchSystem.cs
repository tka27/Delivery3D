using Leopotam.Ecs;


sealed class ResearchSystem : IEcsRunSystem
{
    StaticData staticData;
    SceneData sceneData;
    BuildingsData buildingsData;
    EcsFilter<ResearchLab, Inventory, ProductBuyer> labFilter;
    float timer;



    void IEcsRunSystem.Run()
    {
        if (timer > 0)
        {
            timer--;
            return;
        }
        timer = 20 / sceneData.researchSpeed;

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
                GetReward();
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

    void GetReward()
    {
        switch (staticData.researchLvl)
        {
            case 2:
                sceneData.Notification("Unlocked new building.");
                buildingsData.buildingsInitSystem.BakeryInit();
                buildingsData.buildingsInitSystem.NewProductionBuilding();
                break;

            case 3:
                sceneData.Notification("Unlocked new building.");
                buildingsData.buildingsInitSystem.MeatInit();
                buildingsData.buildingsInitSystem.MilkInit();
                buildingsData.buildingsInitSystem.NewProductionBuilding();
                break;

            case 5:
                sceneData.Notification("Unlocked new building. \nUnlocked new car.");
                buildingsData.buildingsInitSystem.FishInit();
                buildingsData.buildingsInitSystem.NewProductionBuilding();
                staticData.carsUnlockStatus[1] = true;
                break;

            case 7:
                sceneData.Notification("Unlocked new building.");
                buildingsData.buildingsInitSystem.CheeseInit();
                buildingsData.buildingsInitSystem.NewProductionBuilding();
                break;

            case 9:
                sceneData.Notification("Unlocked new building.");
                buildingsData.buildingsInitSystem.CanFishInit();
                buildingsData.buildingsInitSystem.NewProductionBuilding();
                break;

            case 11:
                sceneData.Notification("Unlocked new building. \nUnlocked new car.");
                buildingsData.buildingsInitSystem.ChickenInit();
                buildingsData.buildingsInitSystem.NewProductionBuilding();
                staticData.carsUnlockStatus[2] = true;
                break;

            case 14:
                sceneData.Notification("Unlocked new building.");
                buildingsData.buildingsInitSystem.FruitInit();
                buildingsData.buildingsInitSystem.NewProductionBuilding();
                break;

            case 17:
                sceneData.Notification("Unlocked new building.");
                buildingsData.buildingsInitSystem.VegetablesInit();
                buildingsData.buildingsInitSystem.NewProductionBuilding();
                break;

            case 20:
                sceneData.Notification("Unlocked new building. \nUnlocked new car.");
                buildingsData.buildingsInitSystem.JuiceInit();
                buildingsData.buildingsInitSystem.NewProductionBuilding();
                staticData.carsUnlockStatus[3] = true;
                break;

            case 25:
                sceneData.Notification("Unlocked new building.");
                buildingsData.buildingsInitSystem.IceInit();
                buildingsData.buildingsInitSystem.NewProductionBuilding();
                break;

            case 31:
                sceneData.Notification("Unlocked new building.");
                buildingsData.buildingsInitSystem.PizzaInit();
                buildingsData.buildingsInitSystem.NewProductionBuilding();
                staticData.carsUnlockStatus[4] = true;
                break;

            default: return;
        }



    }

}
