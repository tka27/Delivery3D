using Leopotam.Ecs;
using UnityEngine;


sealed class DestroyRoadSystem : IEcsRunSystem
{

    EcsFilter<PathComp, DestroyRoadRequest> pathFilter;
    EcsFilter<PlayerComp> playerFilter;

    void IEcsRunSystem.Run()
    {
        Vector3 playerPos = new Vector3();
        foreach (var playerF in playerFilter)
        {
            playerPos = playerFilter.Get1(playerF).playerGO.transform.position;
        }
        foreach (var pathF in pathFilter)
        {
            ref var path = ref pathFilter.Get1(pathF);
            path.currentWaypointIndex = 0;
            path.currentPoolIndex = 0;
            
            foreach (var wp in path.wayPoints)
            {
                wp.SetActive(false);
            }
            path.wayPoints.Clear();

            path.lineRenderer.positionCount = 1;
            path.lineRenderer.SetPosition(0, playerPos);
            pathFilter.GetEntity(pathF).Del<DestroyRoadRequest>();
        }
    }
}
