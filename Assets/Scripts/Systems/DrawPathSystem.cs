using Leopotam.Ecs;
using UnityEngine;

sealed class DrawPathSystem : IEcsRunSystem, IEcsInitSystem
{
    EcsFilter<PathComp> pathFilter;
    EcsFilter<Player> playerFilter;
    StaticData staticData;
    SceneData sceneData;
    UIData uiData;
    LayerMask layer;
    Camera camera;

    public void Init()
    {
        layer = LayerMask.GetMask("Ground");
        camera = Camera.main;
    }

    void IEcsRunSystem.Run()
    {
        if (uiData.isPathComplete)
        {
            return;
        }

        var playerPos = new Vector3();
        RaycastHit hit;
        Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
        Vector3 waypointPos;

        if (sceneData.gameMode == GameMode.Build &&
        Input.GetMouseButton(0) &&
        Physics.Raycast(mouseRay, out hit, 1000, layer) &&
        !UIData.IsMouseOverButton(uiData.buttons))
        {
            waypointPos = new Vector3(hit.point.x, hit.point.y + 0.05f, hit.point.z);
            float distanceToNextPoint = 0;

            ref var path = ref pathFilter.Get1(0);
            if (path.wayPoints.Count != 0)
            {
                Vector3 waypointPos0 = new Vector3(waypointPos.x, 0, waypointPos.z);
                Vector3 nextPoint0 = new Vector3(path.wayPoints[path.wayPoints.Count - 1].transform.position.x, 0, path.wayPoints[path.wayPoints.Count - 1].transform.position.z);

                distanceToNextPoint = (waypointPos0 - nextPoint0).magnitude;
            }
            else
            {
                foreach (var f3 in playerFilter)
                {
                    playerPos = playerFilter.Get1(f3).playerGO.transform.position;
                    playerPos.y = playerPos.y - 0.8f;
                }
                distanceToNextPoint = (waypointPos - playerPos).magnitude;
            }


            if (distanceToNextPoint >= 0.2 && distanceToNextPoint <= 10) //distance btw points
            {
                if (path.wayPoints.Count != 0)
                {
                    SetWaypoints(path.wayPoints[path.wayPoints.Count - 1].transform.position, waypointPos);
                    path.lineRenderer.SetPosition(0, path.wayPoints[0].transform.position);
                    if (!uiData.isPathComplete)
                    {
                        CheckPathComplete();
                    }
                }
                else
                {
                    SetWaypoints(playerPos, waypointPos);
                }
            }
        }
    }

    void SetWaypoints(Vector3 first, Vector3 last)
    {
        ref var path = ref pathFilter.Get1(0);
        int iterations = (int)(last - first).magnitude;
        for (int i = 0; i < iterations; i++)
        {
            Vector3 waypointPos = (last - first).normalized + first;
            first = waypointPos;
            path.wayPoints.Add(WPFromPool(waypointPos));
            if (path.lineRenderer.positionCount <= path.wayPoints.Count)
            {
                path.lineRenderer.positionCount++;
            }
            path.lineRenderer.SetPosition(path.wayPoints.Count, path.wayPoints[path.wayPoints.Count - 1].transform.position);
        }
    }
    GameObject WPFromPool(Vector3 pos)
    {
        ref var path = ref pathFilter.Get1(0);
        if (path.currentPoolIndex >= path.waypointsPool.Count)
        {
            path.waypointsPool.Add(GameObject.Instantiate(path.waypointsPool[0]));
        }
        GameObject waypoint = path.waypointsPool[path.currentPoolIndex];
        waypoint.transform.position = pos;
        waypoint.SetActive(true);
        path.currentPoolIndex++;
        return waypoint;
    }

    void CheckPathComplete()
    {
        ref var path = ref pathFilter.Get1(0);
        foreach (var finalPoint in sceneData.finalPoints)
        {
            float distanceToNextPoint = (finalPoint.position - path.wayPoints[path.wayPoints.Count - 1].transform.position).magnitude;
            float distanceToCurrentPoint = (finalPoint.position - path.wayPoints[0].transform.position).magnitude;
            if (distanceToNextPoint < 5 && distanceToCurrentPoint > 5)
            {
                SetWaypoints(path.wayPoints[path.wayPoints.Count - 1].transform.position, finalPoint.position);
                uiData.isPathComplete = true;
            }
        }
    }
}
