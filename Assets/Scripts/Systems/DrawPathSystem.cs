using Leopotam.Ecs;
using UnityEngine;

sealed class DrawPathSystem : IEcsRunSystem, IEcsInitSystem
{
    EcsFilter<PathComp> pathFilter;
    EcsFilter<Player> playerFilter;
    SceneData sceneData;
    UIData uiData;
    LayerMask layer;
    Camera camera;
    const int PATH_STEP = 1;
    const int BRIDGE_POINT_RADIUS = 15;

    public void Init()
    {
        layer = LayerMask.GetMask("Ground");
        camera = Camera.main;
    }

    void IEcsRunSystem.Run()
    {
        if (uiData.isPathComplete) return;

        var playerPos = new Vector3();
        RaycastHit hit;
        Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
        Vector3 waypointPos;

        if (sceneData.gameMode == GameMode.Build &&
        Input.GetMouseButton(0) &&
        Physics.Raycast(mouseRay, out hit, 1000, layer) &&
        !UIData.IsMouseOverButton(uiData.buttons))
        {
            waypointPos = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
            float distanceToNextPoint = 0;

            ref var path = ref pathFilter.Get1(0);
            if (path.wayPoints.Count != 0)
            {
                Vector3 waypointPos0 = new Vector3(waypointPos.x, 0, waypointPos.z);
                Vector3 nextPoint0 = new Vector3(path.wayPoints[path.wayPoints.Count - 1].position.x, 0, path.wayPoints[path.wayPoints.Count - 1].position.z);

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

            if (distanceToNextPoint >= PATH_STEP && distanceToNextPoint <= 15) //distance btw points
            {
                if (path.wayPoints.Count != 0)
                {
                    SetWaypoints(path.wayPoints[path.wayPoints.Count - 1].position, waypointPos);
                    CheckBridges(path);
                    if (!uiData.isPathComplete)
                    {
                        CheckPathComplete(path);
                    }
                }
                else
                {
                    SetWaypoints(playerPos, waypointPos);
                    path.lineRenderer.SetPosition(0, path.wayPoints[0].position);
                }
            }
        }
    }

    void SetWaypoints(Vector3 first, in Vector3 last)
    {
        ref var path = ref pathFilter.Get1(0);
        int iterations = (int)(last - first).magnitude / PATH_STEP;
        for (int i = 0; i < iterations; i++)
        {
            Vector3 waypointPos = (last - first).normalized * PATH_STEP + first;
            first = waypointPos;
            path.wayPoints.Add(WPFromPool(waypointPos, ref path));

            path.lineRenderer.positionCount++;
            path.lineRenderer.SetPosition(path.wayPoints.Count, path.wayPoints[path.wayPoints.Count - 1].position);
        }
    }
    Transform WPFromPool(in Vector3 pos, ref PathComp path)
    {
        if (path.currentPoolIndex >= path.waypointsPool.Count)
        {
            path.waypointsPool.Add(GameObject.Instantiate(path.waypointsPool[0]));
        }
        Transform waypoint = path.waypointsPool[path.currentPoolIndex];
        waypoint.position = pos;
        waypoint.gameObject.SetActive(true);
        path.currentPoolIndex++;
        return waypoint;
    }

    void CheckPathComplete(in PathComp path)
    {
        foreach (var finalPoint in sceneData.finalPoints)
        {
            float distanceToNextPoint = (finalPoint.position - path.wayPoints[path.wayPoints.Count - 1].position).magnitude;
            float distanceToCurrentPoint = (finalPoint.position - path.wayPoints[0].position).magnitude;
            if (distanceToNextPoint < 5 && distanceToCurrentPoint > 5)
            {
                SetWaypoints(path.wayPoints[path.wayPoints.Count - 1].position, finalPoint.position);
                uiData.isPathComplete = true;
            }
        }
    }

    void CheckBridges(in PathComp path)
    {
        for (int i = 0; i < sceneData.freeBridges.Count; i++)
        {
            Bridge bridge = sceneData.freeBridges[i];
            float distanceToBridge = (bridge.point1.position - path.wayPoints[path.wayPoints.Count - 1].position).magnitude;
            if (distanceToBridge < BRIDGE_POINT_RADIUS)
            {
                SetWaypoints(path.wayPoints[path.wayPoints.Count - 1].position, bridge.point1.position);
                SetWaypoints(path.wayPoints[path.wayPoints.Count - 1].position, bridge.point2.position);
                sceneData.freeBridges.Remove(bridge);
                return;
            }
            distanceToBridge = (bridge.point2.position - path.wayPoints[path.wayPoints.Count - 1].position).magnitude;
            if (distanceToBridge < BRIDGE_POINT_RADIUS)
            {
                SetWaypoints(path.wayPoints[path.wayPoints.Count - 1].position, bridge.point2.position);
                SetWaypoints(path.wayPoints[path.wayPoints.Count - 1].position, bridge.point1.position);
                sceneData.freeBridges.Remove(bridge);
                return;
            }
        }
    }

    void DisplayBuildSphere()
    {

    }
}
