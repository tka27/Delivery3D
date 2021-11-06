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
                        foreach (var drivingWheel in player.playerData.drivingWheelColliders)
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
                            drivingWheel.motorTorque = player.currentTorque;
                        }
                        foreach (var steeringWheel in player.playerData.steeringWheelColliders)
                        {
                            steeringWheel.steerAngle = steer;
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
                    for (int i = 0; i < player.playerData.drivingWheelColliders.Count; i++)
                    {
                        player.playerData.drivingWheelColliders[i].motorTorque = 0;
                        player.currentTorque = 0;
                    }
                }
            }
        }
    }
}
