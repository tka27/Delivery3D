using Leopotam.Ecs;
using UnityEngine;


sealed class DestroyRoadSystem : IEcsRunSystem, IEcsInitSystem
{
    EcsFilter<PathComp> pathFilter;
    EcsFilter<PathComp, DestroyRoadRequest> pathRequestFilter;
    EcsFilter<Player> playerFilter;
    UIData uiData;
    SceneData sceneData;
    public void Init()
    {
        ClearPathBtn.clickEvent += AddRequest;
    }

    void IEcsRunSystem.Run()
    {


        foreach (var pathEntity in pathRequestFilter)
        {
            Vector3 playerPos = playerFilter.Get1(0).playerGO.transform.position;
            playerPos.y -= 1;

            ref var path = ref pathRequestFilter.Get1(0);
            path.currentWaypointIndex = 0;
            path.currentPoolIndex = 0;

            foreach (var wp in path.wayPoints)
            {
                wp.gameObject.SetActive(false);
            }
            path.wayPoints.Clear();

            path.lineRenderer.positionCount = 1;
            path.lineRenderer.SetPosition(0, playerPos);
            uiData.isPathComplete = false;
            uiData.isPathConfirmed = false;

            ResetObstacles();
            pathRequestFilter.GetEntity(pathEntity).Del<DestroyRoadRequest>();
        }
    }

    void ResetObstacles()
    {
        sceneData.roadObstaclesCurrentIndex = 0;
        foreach (var obstacle in sceneData.roadObstaclesPool)
        {
            obstacle.SetActive(false);
        }
    }

    void AddRequest()
    {
        pathFilter.GetEntity(0).Get<DestroyRoadRequest>();
    }

}
