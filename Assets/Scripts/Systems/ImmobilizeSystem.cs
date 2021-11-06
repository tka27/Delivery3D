using Leopotam.Ecs;


sealed class ImmobilizeSystem : IEcsRunSystem
{

    EcsFilter<PlayerComp, MovableComp, ImmobilizeRequest> reqestFilter;

    void IEcsRunSystem.Run()
    {
        foreach (var fPlayer in reqestFilter)
        {
            ref var player = ref reqestFilter.Get1(fPlayer);
            player.currentTorque = 0;
            foreach (var wheel in player.playerData.drivingWheelColliders)
            {
                wheel.motorTorque = player.currentTorque;
            }
            reqestFilter.GetEntity(fPlayer).Del<MovableComp>();
        }
    }
}
