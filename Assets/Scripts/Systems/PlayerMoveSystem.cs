using Leopotam.Ecs;
using UnityEngine;


sealed class PlayerMoveSystem : IEcsRunSystem
{
    SceneData sceneData;
    EcsFilter<PlayerComp> playerFilter;
    EcsFilter<PathComp> pathFilter;
    EcsFilter<LineComp> lineFilter;

    void IEcsRunSystem.Run()
    {
        float steer;
        Vector3 tgtPos;
        int currentWaypointIndex = 0;
        foreach (var f1 in playerFilter)
        {
            ref var player = ref playerFilter.Get1(f1);
            foreach (var f2 in pathFilter)
            {
                ref var path = ref pathFilter.Get1(f2);

                if (path.wayPoints.Count != 0 && sceneData.gameMode == GameMode.Drive)
                {
                    tgtPos = path.wayPoints[0].transform.position;
                    tgtPos.y = player.playerData.wheelPos.transform.position.y;
                    float distanceToCurrentPoint = (path.wayPoints[0].transform.position - player.playerData.wheelPos.position).magnitude;
                    if (distanceToCurrentPoint >= 3f)
                    {
                        steer = Vector3.SignedAngle(tgtPos - player.playerData.wheelPos.position, player.playerData.wheelPos.forward, player.playerData.wheelPos.up);
                        steer *= -1;

                        if (steer > player.maxSteerAngle)
                        {
                            steer = player.maxSteerAngle;
                        }
                        else if (steer < -player.maxSteerAngle)
                        {
                            steer = -player.maxSteerAngle;
                        }
                        //move method
                        for (int i = 0; i < player.playerData.wheelColliders.Count; i++)
                        {
                            if (i < 2)
                            {
                                if (Input.GetMouseButton(0) && player.currentSpeed < player.maxTorque)
                                {
                                    player.currentSpeed += player.acceleration;
                                }
                                else
                                {
                                    player.currentSpeed -= player.acceleration;
                                }
                                if (player.currentSpeed < 0)
                                {
                                    player.currentSpeed = 0;
                                }
                                player.playerData.wheelColliders[i].motorTorque = player.currentSpeed; //motor
                                player.playerData.wheelColliders[i].steerAngle = steer;
                            }
                            Vector3 pos;
                            Quaternion quaternion;
                            player.playerData.wheelColliders[i].GetWorldPose(out pos, out quaternion);
                            player.playerData.wheelMeshes[i].transform.position = pos;
                            player.playerData.wheelMeshes[i].transform.rotation = quaternion;
                        }
                    }
                    else
                    {
                        path.wayPoints.Remove(path.wayPoints[0]);
                    }
                }
                else
                {
                    //stop method
                    for (int i = 0; i < player.playerData.wheelColliders.Count; i++)
                    {
                        player.playerData.wheelColliders[i].motorTorque = 0;
                        player.currentSpeed = 0;
                    }
                }


                if (path.wayPoints.Count == 0)
                {
                    foreach (var f3 in lineFilter)
                    {
                        lineFilter.Get1(f3).lineRenderer.positionCount = 1;
                        lineFilter.Get1(f3).lineRenderer.SetPosition(0, player.playerGO.transform.position);
                    }
                }
            }
        }
    }
}
