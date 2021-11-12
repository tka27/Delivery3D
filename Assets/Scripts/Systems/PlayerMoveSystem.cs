using Leopotam.Ecs;
using UnityEngine;


sealed class PlayerMoveSystem : IEcsRunSystem, IEcsInitSystem
{
    SceneData sceneData;
    EcsFilter<PlayerComp, MovableComp> playerFilter;
    EcsFilter<PathComp> pathFilter;
    UIData uiData;
    EcsWorld _world;
    Camera camera;
    Transform buildCameraPos;

    public void Init()
    {
        camera = Camera.main;
        buildCameraPos = sceneData.buildCam.transform;
    }

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
                    tgtPos.y = player.carData.wheelPos.transform.position.y;
                    float distanceToCurrentPoint = (path.wayPoints[path.currentWaypointIndex].transform.position - player.carData.wheelPos.position).magnitude;


                    for (int i = path.currentWaypointIndex; i < path.wayPoints.Count; i++) //calc nearest point
                    {
                        float checkDist = (path.wayPoints[i].transform.position - player.carData.wheelPos.position).magnitude;
                        if (checkDist < distanceToCurrentPoint)
                        {
                            path.currentWaypointIndex = i;
                        }
                    }


                    if (distanceToCurrentPoint >= 3f)
                    {
                        Vector3 tgtAt0 = new Vector3(tgtPos.x, 0, tgtPos.z);
                        Vector3 playerAt0 = new Vector3(player.carData.wheelPos.position.x, 0, player.carData.wheelPos.position.z);

                        Vector3 yNormal = Vector3.Cross(Vector3.forward - playerAt0, Vector3.right - playerAt0);
                        if (yNormal.y < 0)
                        {
                            yNormal *= -1;
                        }
                        Vector3 projectToXZ = Vector3.ProjectOnPlane(player.carData.wheelPos.forward, yNormal);

                        steer = Vector3.SignedAngle(tgtAt0 - playerAt0, projectToXZ, yNormal);
                        steer *= -1;


                        if (steer > player.maxSteerAngle)
                        {
                            steer = player.maxSteerAngle;
                        }
                        else if (steer < -player.maxSteerAngle)
                        {
                            steer = -player.maxSteerAngle;
                        }




                        
                        if (Input.GetMouseButton(0))//move
                        {
                            if (player.currentTorque < player.maxTorque - player.acceleration)
                            {
                                player.currentTorque += player.acceleration;
                            }
                            foreach (var wheel in player.carData.allWheelColliders)
                            {
                                wheel.brakeTorque = 0;
                            }

                        }
                        else    //stop
                        {
                            player.currentTorque = 0;
                            foreach (var brakingWheel in player.carData.brakingWheelColliders)
                            {
                                brakingWheel.brakeTorque = player.maxTorque / 300;
                                brakingWheel.motorTorque = 0;
                            }
                        }
                        foreach (var drivingWheel in player.carData.drivingWheelColliders)
                        {
                            drivingWheel.motorTorque = player.currentTorque;
                        }



                        foreach (var steeringWheel in player.carData.steeringWheelColliders)
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
                            float cameraHeight = buildCameraPos.position.y;
                            Vector3 pos = new Vector3(player.playerGO.transform.position.x, cameraHeight, player.playerGO.transform.position.z);
                            buildCameraPos.position = pos;
                            sceneData.gameMode = GameMode.View;
                            uiData.gameModeText.text = sceneData.gameMode.ToString();
                        }

                    }
                }
                else
                {
                    //stop method
                    player.currentTorque = 0;
                    for (int i = 0; i < player.carData.drivingWheelColliders.Count; i++)
                    {
                        player.carData.drivingWheelColliders[i].motorTorque = 0;
                        player.carData.drivingWheelColliders[i].brakeTorque = player.carData.maxTorque;
                    }
                }
            }
        }
    }
}
