using Leopotam.Ecs;
using UnityEngine;


sealed class DestroyRoadSystem : IEcsRunSystem
{
    EcsFilter<PathComp> monoRequestFilter;
    EcsFilter<PathComp, DestroyRoadRequest> pathFilter;
    EcsFilter<PlayerComp> playerFilter;
    SceneData sceneData;
    UIData uiData;

    void IEcsRunSystem.Run()
    {
        Vector3 playerPos = new Vector3();
        foreach (var playerF in playerFilter)
        {
            playerPos = playerFilter.Get1(playerF).playerGO.transform.position;
            playerPos.y -= 1;
        }
        foreach (var fPath in pathFilter)
        {
            ref var path = ref pathFilter.Get1(fPath);
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
            pathFilter.GetEntity(fPath).Del<DestroyRoadRequest>();
        }
        if (!uiData.clearPathRequest)
        {
            return;
        }
        uiData.clearPathRequest = false;
        foreach (var fPath in monoRequestFilter)
        {
            monoRequestFilter.GetEntity(fPath).Get<DestroyRoadRequest>();
        }
    }
}
