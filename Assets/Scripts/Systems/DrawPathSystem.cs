using Leopotam.Ecs;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


sealed class DrawPathSystem : IEcsRunSystem, IEcsInitSystem
{
    EcsFilter<PathComp> pathFilter;
    EcsFilter<LineComp> lineFilter;
    StaticData staticData;
    SceneData sceneData;
    EcsWorld _world;
    LayerMask layer;

    public void Init()
    {
        layer = LayerMask.GetMask("Ground");
    }

    void IEcsRunSystem.Run()
    {
        RaycastHit hit;
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 waypointPos;

        if (sceneData.gameMode == GameMode.Build && 
        Input.GetMouseButton(0) && 
        Physics.Raycast(mouseRay, out hit, 1000, layer) && 
        !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) //check ui button
        {
            waypointPos = new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z);
            foreach (var f2 in pathFilter)
            {
                ref var path = ref pathFilter.Get1(f2);
                float distanceToNextPoint = 0;
                if (path.wayPoints.Count != 0)
                {
                    distanceToNextPoint = (waypointPos - path.wayPoints[path.wayPoints.Count - 1].transform.position).magnitude;
                }
                else
                {
                    distanceToNextPoint = 1;
                }
                if (distanceToNextPoint >= 0.2 && distanceToNextPoint <= 2) //distance btw points
                {
                    path.wayPoints.Add(GameObject.Instantiate(staticData.pathPoint, waypointPos, Quaternion.identity));
                    foreach (var f3 in lineFilter)
                    {
                        ref var line = ref lineFilter.Get1(f3);
                        line.lineRenderer.SetPosition(0, path.wayPoints[0].transform.position);
                        if (line.lineRenderer.positionCount <= path.wayPoints.Count)
                        {
                            line.lineRenderer.positionCount++;
                        }
                        line.lineRenderer.SetPosition(path.wayPoints.Count, path.wayPoints[path.wayPoints.Count - 1].transform.position);
                    }
                }
            }
        }
    }
}
