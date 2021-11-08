using Leopotam.Ecs;
using UnityEngine;


sealed class FarmProduceSystem : IEcsRunSystem
{

    EcsFilter<ProductSeller, StorageComp>.Exclude<ProductBuyer> producerFilter;

    void IEcsRunSystem.Run()
    {
        foreach (var fProd in producerFilter)
        {
            ref var producer = ref producerFilter.Get1(fProd);

            producer.timer--;
            if (producer.timer > 0)
            {
                continue;
            }
            producer.timer = 50 / producer.produceSpeed;

            ref var storage = ref producerFilter.Get2(fProd);
            if (storage.currentMass < storage.maxMass)
            {
                producer.product.mass++;
                storage.currentMass = producer.product.mass;
                producerFilter.GetEntity(fProd).Get<SellDataUpdateRequest>();
            }
        }
    }
}
