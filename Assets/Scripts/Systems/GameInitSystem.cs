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
    private RuntimeData runtimeData; //юзать эту дату на ентити gameobject

    public void Init()
    {
        var inputEntity = _world.NewEntity();
        inputEntity.Get<InputComp>();

        var pathEntity = _world.NewEntity();
        ref var pathComp = ref pathEntity.Get<PathComp>();
        pathComp.wayPoints = new List<GameObject>();

        var playerEntity = _world.NewEntity();
        ref var playerComp = ref playerEntity.Get<PlayerComp>();
        GameObject playerGO = Object.Instantiate(staticData.playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        playerComp.playerGO = playerGO;
        playerComp.wheelsRB = playerGO.transform.GetChild(0).GetComponent<Rigidbody2D>();
        playerEntity.Get<InputComp>();
        playerEntity.Get<MovableComp>();

        var lineEntity = _world.NewEntity();
        var lineGO = Object.Instantiate(staticData.linePrefab);
        lineEntity.Get<LineComp>().lineRenderer = lineGO.GetComponent<LineRenderer>();
    }
}
