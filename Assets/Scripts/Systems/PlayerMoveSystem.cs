using Leopotam.Ecs;
using UnityEngine;


sealed class PlayerMoveSystem : IEcsRunSystem
{
    RuntimeData runtimeData;
    EcsFilter<MovableComp, InputComp, PlayerComp> playerFilter;
    EcsFilter<PathComp> pathFilter;
    EcsFilter<LineComp> lineFilter;

    void IEcsRunSystem.Run()
    {
        foreach (var f1 in playerFilter)
        {
            ref var movable = ref playerFilter.Get1(f1);
            ref var input = ref playerFilter.Get2(f1);
            ref var player = ref playerFilter.Get3(f1);
            foreach (var f2 in pathFilter)
            {
                ref var path = ref pathFilter.Get1(f2);

                if (input.spaceDown && movable.moveSpeed < 1500)
                {
                    movable.moveSpeed += 100 * Time.deltaTime;
                }
                else if (movable.moveSpeed >= 10)
                {
                    movable.moveSpeed -= 100 * Time.deltaTime;
                }
                else
                {
                    movable.moveSpeed = 0;
                }
                Debug.Log(movable.moveSpeed);////////////

                if (path.wayPoints.Count != 0)
                {
                    float distanceToCurrentPoint = (path.wayPoints[0].transform.position - player.playerGO.transform.position).magnitude;
                    if (distanceToCurrentPoint >= 1.5f)
                    {
                        player.wheelsRB.AddForce((path.wayPoints[0].transform.position - player.playerGO.transform.position).normalized * movable.moveSpeed * Time.deltaTime);
                    }
                    else
                    {
                        GameObject.Destroy(path.wayPoints[0]);
                        path.wayPoints.Remove(path.wayPoints[0]);
                    }
                }
                else
                {
                    movable.moveSpeed = 0;
                }


                if (path.wayPoints.Count == 0)
                    foreach (var f3 in lineFilter)
                    {
                        lineFilter.Get1(f3).lineRenderer.positionCount = 1;
                        lineFilter.Get1(f3).lineRenderer.SetPosition(0, player.playerGO.transform.position);
                    }
            }
        }
    }
}
