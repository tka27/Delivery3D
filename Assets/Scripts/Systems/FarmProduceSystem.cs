using Leopotam.Ecs;
using UnityEngine;


sealed class FarmProduceSystem : IEcsRunSystem
{

    EcsFilter<ProductSeller, StorageComp> farmFilter;
    float timer;

    void IEcsRunSystem.Run()
    {
        timer--;
        if (timer > 0)
        {
            return;
        }
        foreach (var fFarm in farmFilter)
        {
            ref var farm = ref farmFilter.Get1(fFarm);
            ref var storage = ref farmFilter.Get2(fFarm);
            if (storage.currentMass < storage.maxMass)
            {
                farm.sellingProductCount++;
                storage.currentMass++;
                farm.tradePointData.storageInfo.text = storage.currentMass + " / " + storage.maxMass;
            }

            timer = 50 / farm.produceSpeed;
        }
    }
}
