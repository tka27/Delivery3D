using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private StaticData staticData; // мы можем добавить новые ссылки на StaticData и SceneData
    private SceneData sceneData;
    //private RuntimeData runtimeData; //юзать эту дату на gameobject

    public void Init()
    {
        var pathEntity = _world.NewEntity();
        ref var pathComp = ref pathEntity.Get<PathComp>();
        pathComp.wayPoints = new List<GameObject>();
        pathComp.lineRenderer = sceneData.lineRenderer;

        var playerEntity = _world.NewEntity();
        ref var playerComp = ref playerEntity.Get<PlayerComp>();
        GameObject playerGO = sceneData.car;//Object.Instantiate(staticData.playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        playerComp.playerGO = playerGO;
        playerComp.playerData = playerGO.GetComponent<PlayerData>();
        playerComp.playerGO.GetComponent<Rigidbody>().centerOfMass = playerComp.playerData.centerOfMass.transform.localPosition;
        playerComp.maxSteerAngle = 45;
        playerComp.maxTorque = 10000;
        playerComp.acceleration = 50;
    }
}
