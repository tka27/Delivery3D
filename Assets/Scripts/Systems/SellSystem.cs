using Leopotam.Ecs;
using UnityEngine;
using System.Collections.Generic;


sealed class SellSystem : IEcsRunSystem
{

    EcsFilter<ProductBuyer, Inventory> buyerFilter;
    EcsFilter<Inventory, PlayerComp> playerFilter;
    UIData uiData;
    StaticData staticData;

    void IEcsRunSystem.Run()
    {
        foreach (var fBuyer in buyerFilter)
        {
            ref var buyer = ref buyerFilter.Get1(fBuyer);
            if (buyer.tradePointData.ableToTrade && uiData.sellRequest)
            {
                uiData.sellRequest = false;
                ref var buyerInventory = ref buyerFilter.Get2(fBuyer);
                foreach (var fPlayer in playerFilter)
                {
                    ref var playerInventory = ref playerFilter.Get1(fPlayer);
                    ref var player = ref playerFilter.Get2(fPlayer);

                    List<int> playerIndexes = new List<int>();
                    List<int> buyerIndexes = new List<int>();

                    foreach (var buyingProductType in buyer.buyingProductTypes)
                    {
                        for (int i = 0; i < playerInventory.inventory.Count; i++) // check all player products & buyer required products
                        {
                            for (int j = 0; j < buyerInventory.inventory.Count; j++)
                            {
                                if (playerInventory.inventory[i].type == buyerInventory.inventory[j].type && buyerInventory.inventory[j].type == buyingProductType)
                                {
                                    playerIndexes.Add(i);
                                    buyerIndexes.Add(j);// add indexes of same products
                                }
                            }
                        }
                    }



                    float playerActualProductsMass = 0;
                    for (int i = 0; i < playerIndexes.Count; i++)
                    {
                        playerActualProductsMass += playerInventory.inventory[playerIndexes[i]].mass;
                    }
                    if (playerActualProductsMass == 0)
                    {
                        return;
                    }
                    float totalCost = 0;

                    float buyerCurrentMass = buyerInventory.GetCurrentMass();
                    float buyerFreeSpace = buyerInventory.maxMass - buyerCurrentMass;

                    if (playerActualProductsMass <= buyerFreeSpace) // sell all
                    {
                        for (int i = 0; i < playerIndexes.Count; i++)
                        {
                            Debug.Log(buyerInventory.inventory[buyerIndexes[i]].mass);
                            buyerInventory.inventory[buyerIndexes[i]].mass += playerInventory.inventory[playerIndexes[i]].mass;
                            playerInventory.inventory[playerIndexes[i]].mass = 0;
                            totalCost += buyerInventory.inventory[buyerIndexes[i]].mass * buyerInventory.inventory[buyerIndexes[i]].currentPrice;
                        }
                    }
                    else if (playerActualProductsMass > buyerFreeSpace)
                    {
                        float eachProductMass = buyerFreeSpace / playerIndexes.Count;
                        for (int i = 0; i < playerIndexes.Count; i++)
                        {
                            buyerInventory.inventory[buyerIndexes[i]].mass += eachProductMass;
                            playerInventory.inventory[playerIndexes[i]].mass -= eachProductMass;
                            totalCost += eachProductMass * buyerInventory.inventory[buyerIndexes[i]].currentPrice;
                        }
                    }
                    staticData.currentMoney += totalCost;

                    buyerFilter.GetEntity(fBuyer).Get<BuyDataUpdateRequest>();
                    buyerFilter.GetEntity(fBuyer).Get<SellDataUpdateRequest>();
                    playerFilter.GetEntity(0).Get<UpdateCargoRequest>();
                    playerInventory.RemoveEmptySlots();
                    foreach (var go in player.carData.playerCargo)
                    {
                        go.SetActive(false);
                    }
                }
            }
        }
    }
}

