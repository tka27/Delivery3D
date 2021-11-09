using Leopotam.Ecs;


sealed class ResearchSystem : IEcsRunSystem
{

    EcsFilter<ResearchLab, StorageComp, ProductBuyer> labFilter;

    void IEcsRunSystem.Run()
    {
        foreach (var fLab in labFilter)
        {
            
        }
    }
}
