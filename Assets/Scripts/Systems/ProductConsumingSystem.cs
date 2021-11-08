using Leopotam.Ecs;


sealed class ProductConsumingSystem : IEcsRunSystem
{

    EcsFilter<ProductBuyer, StorageComp>.Exclude<ProductSeller> consumerFilter;
    float timer;
    void IEcsRunSystem.Run()
    {
        timer--;
        if (timer > 0)
        {
            return;
        }
        timer = 50;

        foreach (var fCons in consumerFilter)
        {
            ref var buyer = ref consumerFilter.Get1(fCons);
            ref var storage = ref consumerFilter.Get2(fCons);
            if (storage.currentMass > 0)
            {
                storage.currentMass--;
                consumerFilter.GetEntity(fCons).Get<BuyDataUpdateRequest>();
            }
        }
    }
}