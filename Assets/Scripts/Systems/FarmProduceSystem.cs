using Leopotam.Ecs;
using UnityEngine;


sealed class FarmProduceSystem : IEcsRunSystem
{

    EcsFilter<Farm, CargoComp> farmFilter;
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
            ref var cargo = ref farmFilter.Get2(fFarm);
            if (cargo.currentWeight < cargo.maxWeight)
            {
                farm.sellingProductCount++;
                cargo.currentWeight++;
                Debug.Log(farm.sellingProductCount);
            }

            timer = 50 / farm.produceSpeed;
        }
    }
}
