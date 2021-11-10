using Leopotam.Ecs;


sealed class ProductConsumingSystem : IEcsRunSystem
{

        //////////////////OFF//////////////
    EcsFilter<ProductBuyer, Inventory>.Exclude<ProductSeller> consumerFilter;
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
            ref var buyerInventory = ref consumerFilter.Get2(fCons);
            if (buyerInventory.GetCurrentMass() > 0)
            {
                foreach (var product in buyer.buyingProductTypes)
                {
                    foreach (var inventoryItem in buyerInventory.inventory)
                    {
                        if(product==inventoryItem.type){
                            inventoryItem.mass--;
                        }
                    }//fixed
                }
                consumerFilter.GetEntity(fCons).Get<BuyDataUpdateRequest>();
            }
        }
    }
}