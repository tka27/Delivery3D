using Leopotam.Ecs;
using UnityEngine;


sealed class PlayerMoveSystem : IEcsRunSystem
{
    SceneData sceneData;
    EcsFilter<PlayerComp, MovableComp> playerFilter;
    EcsFilter<PathComp> pathFilter;
    EcsWorld _world;

    void IEcsRunSystem.Run()
    {
        foreach (var playerF in playerFilter)
        {
            float steer;
            Vector3 tgtPos;
            ref var player = ref playerFilter.Get1(playerF);
            foreach (var pathF in pathFilter)
            {
                ref var path = ref pathFilter.Get1(pathF);

                if (path.wayPoints.Count != 0 && sceneData.gameMode == GameMode.Drive)
                {
                    tgtPos = path.wayPoints[path.currentWaypointIndex].transform.position;
                    tgtPos.y = player.playerData.wheelPos.transform.position.y;
                    float distanceToCurrentPoint = (path.wayPoints[path.currentWaypointIndex].transform.position - player.playerData.wheelPos.position).magnitude;
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
                                if (Input.GetMouseButton(0) && player.currentTorque < player.maxTorque)
                                {
                                    player.currentTorque += player.acceleration;
                                }
                                else
                                {
                                    player.currentTorque -= player.acceleration;
                                }
                                if (player.currentTorque < 0)
                                {
                                    player.currentTorque = 0;
                                }
                                player.playerData.wheelColliders[i].motorTorque = player.currentTorque; //motor
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
                        if (path.currentWaypointIndex < path.wayPoints.Count - 1)
                        {
                            path.currentWaypointIndex++;
                        }
                        else
                        {
                            pathFilter.GetEntity(pathF).Get<DestroyRoadRequest>();
                            sceneData.gameMode = GameMode.View;
                            sceneData.gameModeText.text = sceneData.gameMode.ToString();
                        }

                    }
                }
                else
                {
                    //stop method
                    for (int i = 0; i < player.playerData.wheelColliders.Count; i++)
                    {
                        player.playerData.wheelColliders[i].motorTorque = 0;
                        player.currentTorque = 0;
                    }
                }


                /*if (path.wayPoints.Count == 0)
                {
                    foreach (var lineF in lineFilter)
                    {
                        lineFilter.Get1(lineF).lineRenderer.positionCount = 1;
                        lineFilter.Get1(lineF).lineRenderer.SetPosition(0, player.playerGO.transform.position);
                    }
                }*/
            }
        }
    }
}
