using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private StaticData staticData;
    private SceneData sceneData;

    public void Init()
    {
        var pathEntity = _world.NewEntity();
        ref var pathComp = ref pathEntity.Get<PathComp>();
        pathComp.wayPoints = new List<GameObject>();
        pathComp.lineRenderer = sceneData.lineRenderer;
        pathComp.waypointsPool = sceneData.waypointsPool;

        var playerEntity = _world.NewEntity();
        ref var playerComp = ref playerEntity.Get<PlayerComp>();
        playerEntity.Get<MovableComp>();
        GameObject playerGO = sceneData.car;
        playerComp.playerGO = playerGO;
        playerComp.playerData = playerGO.GetComponent<PlayerData>();
        playerComp.playerRB = playerComp.playerGO.GetComponent<Rigidbody>();
        playerComp.playerRB.centerOfMass = playerComp.playerData.centerOfMass.transform.localPosition;
        playerComp.maxSteerAngle = 45;
        playerComp.maxTorque = 10000;
        playerComp.acceleration = 50;
        playerComp.maxHealth = 100;
        playerComp.currentHealth = playerComp.maxHealth;
        playerComp.maxFuel = 100;
        playerComp.currentFuel = playerComp.maxFuel;
        playerComp.fuelConsumption = 0.01f;

        var wheatFarmEntity = _world.NewEntity();
        ref var wheatFactory = ref wheatFarmEntity.Get<Farm>();
        wheatFactory.sellingProduct = ProductType.Wheat;
        //wheatFactory.sellPrice = 0;
        wheatFactory.produceSpeed = 80;
        ref var wheatCargo = ref wheatFarmEntity.Get<CargoComp>();
        wheatCargo.maxWeight = 500;
        //wheatCargo.currentWeight = 0;
    }
}
