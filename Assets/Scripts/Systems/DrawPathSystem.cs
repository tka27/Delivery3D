using Leopotam.Ecs;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


sealed class DrawPathSystem : IEcsRunSystem, IEcsInitSystem
{
    EcsFilter<PathComp> pathFilter;
    EcsFilter<PlayerComp> playerFilter;
    Vector3 playerPos;
    StaticData staticData;
    SceneData sceneData;
    LayerMask layer;
    Camera camera;

    public void Init()
    {
        layer = LayerMask.GetMask("Ground");
        camera = Camera.main;
        playerPos = new Vector3();

    }

    void IEcsRunSystem.Run()
    {

        RaycastHit hit;
        Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
        Vector3 waypointPos;

        if (sceneData.gameMode == GameMode.Build &&
        Input.GetMouseButton(0) &&
        Physics.Raycast(mouseRay, out hit, 1000, layer) &&
        !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) //check ui button
        {
            waypointPos = new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z);

            float distanceToNextPoint = 0;
            foreach (var pathF in pathFilter)
            {
                ref var path = ref pathFilter.Get1(pathF);
                if (path.wayPoints.Count != 0)
                {
                    distanceToNextPoint = (waypointPos - path.wayPoints[path.wayPoints.Count - 1].transform.position).magnitude;
                }
                else
                {
                    foreach (var f3 in playerFilter)
                    {
                        playerPos = playerFilter.Get1(f3).playerGO.transform.position;
                        playerPos.y = -0.99f;
                    }
                    distanceToNextPoint = (waypointPos - playerPos).magnitude;
                }
                if (distanceToNextPoint >= 0.2 && distanceToNextPoint <= 10) //distance btw points
                {
                    if (path.wayPoints.Count != 0)
                    {
                        SetWaypoints(path.wayPoints[path.wayPoints.Count - 1].transform.position, waypointPos);
                        path.lineRenderer.SetPosition(0, path.wayPoints[0].transform.position);
                    }
                    else
                    {
                        SetWaypoints(playerPos, waypointPos);
                    }
                }
            }
        }
    }

    void SetWaypoints(Vector3 first, Vector3 last)
    {
        foreach (var pathF in pathFilter)
        {
            ref var path = ref pathFilter.Get1(pathF);
            int iterations = (int)(last - first).magnitude;
            for (int i = 0; i < iterations; i++)
            {
                Vector3 waypointPos = (last - first).normalized + first;
                first = waypointPos;
                path.wayPoints.Add(WPFromPool(waypointPos));//GameObject.Instantiate(staticData.pathPoint, waypointPos, Quaternion.identity));

                if (path.lineRenderer.positionCount <= path.wayPoints.Count)
                {
                    path.lineRenderer.positionCount++;
                }
                path.lineRenderer.SetPosition(path.wayPoints.Count, path.wayPoints[path.wayPoints.Count - 1].transform.position);
            }
        }
    }
    GameObject WPFromPool(Vector3 pos)
    {
        foreach (var pathF in pathFilter)
        {
            ref var path = ref pathFilter.Get1(pathF);
            GameObject waypoint = path.waypointsPool[path.currentPoolIndex];
            waypoint.transform.position = pos;
            waypoint.SetActive(true);
            path.currentPoolIndex++;
            return waypoint;
        }
        return null;
    }
}
