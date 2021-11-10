using Leopotam.Ecs;


sealed class ResearchSystem : IEcsRunSystem
{

    EcsFilter<ResearchLab, Inventory, ProductBuyer> labFilter;

    void IEcsRunSystem.Run()
    {
        foreach (var fLab in labFilter)
        {

        }
    }
}
