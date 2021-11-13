using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;


sealed class ResearchSystem : IEcsRunSystem
{
    StaticData staticData;
    ProductData productData;
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

        foreach (var fLab in labFilter)
        {
            ref var lab = ref labFilter.Get1(fLab);
            ref var labInventory = ref labFilter.Get2(fLab);
            ref var labBuyer = ref labFilter.Get3(fLab);


            bool haveProduct = false;
            foreach (var inventoryItem in labInventory.inventory)
            {
                if (inventoryItem.type == labBuyer.buyingProductTypes[0])
                {
                    haveProduct = true;
                }
            }
            if (!haveProduct) return;

            Debug.Log(123);
            if (labInventory.inventory[0].mass > 0)
            {
                labInventory.inventory[0].mass--;
                lab.progress++;
            }
            else return;



            if (lab.progress >= lab.requirement)
            {
                staticData.researchLvl++;
                if (staticData.researchLvl >= sceneData.researchList.Count)
                {
                    Debug.Log("Max research lvl");
                    labFilter.GetEntity(fLab).Del<ResearchLab>();
                    labBuyer.tradePointData.labProgress.text = "Max lvl";
                    return;
                }
                lab.progress = 0;
                lab.requirement += lab.requirement / 2 * staticData.researchLvl;
                labBuyer.buyingProductTypes.Clear();
                labBuyer.buyingProductTypes.Add(sceneData.researchList[staticData.researchLvl].type);
                labInventory.inventory.Clear();
                labInventory.inventory.Add(sceneData.researchList[staticData.researchLvl]);
            }

            labBuyer.tradePointData.labProgress.text = lab.progress.ToString() + "/" + lab.requirement.ToString();
            labFilter.GetEntity(fLab).Get<BuyDataUpdateRequest>();
        }
    }
}
