using Leopotam.Ecs;
using UnityEngine;


sealed class FarmProduceSystem : IEcsRunSystem
{

    EcsFilter<ProductSeller, StorageComp>.Exclude<ProductBuyer> producerFilter;
    float timer;

    void IEcsRunSystem.Run()
    {
        timer--;
        if (timer > 0)
        {
            return;
        }
        foreach (var fProd in producerFilter)
        {
            ref var producer = ref producerFilter.Get1(fProd);
            ref var storage = ref producerFilter.Get2(fProd);
            if (storage.currentMass < storage.maxMass)
            {
                producer.product.mass++;
                storage.currentMass = producer.product.mass;
                producerFilter.GetEntity(fProd).Get<SellDataUpdateRequest>();
            }

            timer = 50 / producer.produceSpeed;
        }
    }
}
