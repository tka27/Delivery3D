using Leopotam.Ecs;
using UnityEngine;


sealed class DestroyRoadSystem : IEcsRunSystem
{
    EcsFilter<PathComp> pathFilter;
    EcsFilter<PathComp, DestroyRoadRequest> pathRequestFilter;
    EcsFilter<PlayerComp> playerFilter;
    UIData uiData;

    void IEcsRunSystem.Run()
    {
        Vector3 playerPos = new Vector3();
        foreach (var playerF in playerFilter)
        {
            playerPos = playerFilter.Get1(playerF).playerGO.transform.position;
            playerPos.y -= 1;
        }
        foreach (var fPath in pathRequestFilter)
        {
            ref var path = ref pathRequestFilter.Get1(fPath);
            path.currentWaypointIndex = 0;
            path.currentPoolIndex = 0;

            foreach (var wp in path.wayPoints)
            {
                wp.SetActive(false);
            }
            path.wayPoints.Clear();

            path.lineRenderer.positionCount = 1;
            path.lineRenderer.SetPosition(0, playerPos);
            uiData.isPathComplete = false;
            uiData.isPathConfirmed = false;
            pathRequestFilter.GetEntity(fPath).Del<DestroyRoadRequest>();
        }
        if (!uiData.clearPathRequest) return;

        uiData.clearPathRequest = false;
        pathFilter.GetEntity(0).Get<DestroyRoadRequest>();

    }
}
