using Leopotam.Ecs;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


sealed class DrawPathSystem : IEcsRunSystem
{

    EcsFilter<InputComp> inputFilter;
    EcsFilter<PathComp> pathFilter;
    EcsFilter<LineComp> lineFilter;
    StaticData staticData;
    EcsWorld _world;

    void IEcsRunSystem.Run()
    {

        foreach (var f1 in inputFilter)
        {
            ref var input = ref inputFilter.Get1(f1);
            if (input.mouse0Down)
            {
                foreach (var f2 in pathFilter)
                {
                    ref var path = ref pathFilter.Get1(f2);
                    float distanceToNextPoint = 0;
                    if (path.wayPoints.Count != 0)
                    {
                        distanceToNextPoint = (input.mouseWorldPos - path.wayPoints[path.wayPoints.Count - 1].transform.position).magnitude;
                    }
                    else
                    {
                        distanceToNextPoint = 1;
                    }
                    if (distanceToNextPoint >= 0.2 && distanceToNextPoint <= 1)
                    {
                        path.wayPoints.Add(GameObject.Instantiate(staticData.pathPoint, input.mouseWorldPos, Quaternion.identity));
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
}
