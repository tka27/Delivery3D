using Leopotam.Ecs;
using UnityEngine;


sealed class FactoryProduceSystem : IEcsRunSystem
{

    EcsFilter<ProductSeller, StorageComp, ProductBuyer> producerFilter;
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
            ref var consumer = ref producerFilter.Get3(fProd);
            if (storage.currentMass <= storage.maxMass && consumer.product.mass != 0)
            {
                producer.product.mass++;
                consumer.product.mass--;
                producerFilter.GetEntity(fProd).Get<BuyDataUpdateRequest>();
                producerFilter.GetEntity(fProd).Get<SellDataUpdateRequest>();
            }
            timer = 50 / producer.produceSpeed;
        }
    }
}
