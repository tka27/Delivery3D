using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;


public class GameInitSystem : IEcsInitSystem
{
    EcsWorld _world;
    SceneData sceneData;
    StaticData staticData;
    UIData uiData;
    ProductData productData;
    public void Init()
    {


        staticData.currentMoney = staticData.moneyForGame;
        uiData.moneyText.text = staticData.currentMoney.ToString("0");
        var pathEntity = _world.NewEntity();
        ref var pathComp = ref pathEntity.Get<PathComp>();
        pathComp.wayPoints = new List<GameObject>();
        pathComp.lineRenderer = sceneData.lineRenderer;
        pathComp.waypointsPool = sceneData.waypointsPool;

        var playerEntity = _world.NewEntity();
        ref var playerComp = ref playerEntity.Get<PlayerComp>();
        playerEntity.Get<MovableComp>();
        sceneData.cars[staticData.selectedCarID].SetActive(true);
        playerComp.playerGO = sceneData.cars[staticData.selectedCarID];
        //playerComp.playerGO = GameObject.Instantiate(staticData.carsPrefabs[staticData.selectedCarID]);
        playerComp.carData = playerComp.playerGO.GetComponent<CarData>();
        playerComp.playerRB = playerComp.playerGO.GetComponent<Rigidbody>();
        playerComp.defaultRBMass = playerComp.playerRB.mass;
        playerComp.playerRB.centerOfMass = playerComp.carData.centerOfMass.transform.localPosition;
        playerComp.maxSteerAngle = playerComp.carData.maxSteerAngle;// 45;
        playerComp.maxTorque = playerComp.carData.maxTorque;// 10000;
        playerComp.acceleration = playerComp.carData.acceleration;// 50;
        playerComp.maxDurability = playerComp.carData.maxDurability;// 100;
        playerComp.currentDurability = playerComp.maxDurability;
        uiData.durabilityText.text = playerComp.currentDurability.ToString();
        playerComp.maxFuel = playerComp.carData.maxFuel;// 100;
        playerComp.currentFuel = playerComp.maxFuel;
        uiData.fuelText.text = playerComp.currentFuel.ToString();
        playerComp.fuelConsumption = playerComp.carData.fuelConsumption;// 0.01f;
        playerEntity.Get<CargoComp>().inventory = new List<Product>();
        playerEntity.Get<StorageComp>().maxMass = playerComp.carData.maxStorageMass;// 50;
        playerEntity.Get<UpdateCargoRequest>();
        for (int i = 0; i < playerComp.carData.playerCargo.Count; i++)
        {
            playerComp.carData.playerCargoRB.Add(playerComp.carData.playerCargo[i].gameObject.GetComponent<Rigidbody>());
            playerComp.carData.playerCargoDefaultPos.Add(playerComp.carData.playerCargo[i].transform.localPosition);
            playerComp.carData.playerCargoDefaultRot.Add(playerComp.carData.playerCargo[i].transform.localRotation);
        }

        var virtualCam = sceneData.driveCam.GetComponent<CinemachineVirtualCamera>();
        virtualCam.Follow = playerComp.playerGO.transform;
        virtualCam.LookAt = playerComp.carData.cameraLookPoint;


        var wheatFarmEntity = _world.NewEntity();
        ref var wheatFarm = ref wheatFarmEntity.Get<ProductSeller>();
        wheatFarm.sellerGO = sceneData.wheatFarmTradePoint;
        wheatFarm.tradePointData = wheatFarm.sellerGO.GetComponent<TradePointData>();
        wheatFarm.produceSpeed = 0.5f * 10;
        wheatFarm.product = new Product(ProductType.Wheat, productData.wheat, 0.5f);
        wheatFarm.repriceMultiplier = 1.2f;
        wheatFarmEntity.Get<StorageComp>().maxMass = 200;
        wheatFarmEntity.Get<SellDataUpdateRequest>();

        var bakeryEntity = _world.NewEntity();
        ref var bakeryBuyer = ref bakeryEntity.Get<ProductBuyer>();
        bakeryBuyer.buyerGO = sceneData.bakeryTradePoint;
        bakeryBuyer.tradePointData = bakeryBuyer.buyerGO.GetComponent<TradePointData>();
        bakeryBuyer.product = new Product(ProductType.Wheat, productData.wheat, 0.75f);
        bakeryBuyer.repriceMultiplier = 1.2f;
        bakeryEntity.Get<StorageComp>().maxMass = 200;
        ref var bakerySeller = ref bakeryEntity.Get<ProductSeller>();
        bakerySeller.produceSpeed = 1;
        bakerySeller.sellerGO = bakeryBuyer.buyerGO;
        bakerySeller.product = new Product(ProductType.Bread, productData.bread, 1.33f);
        bakerySeller.repriceMultiplier = 1.2f;
        bakerySeller.tradePointData = bakeryBuyer.tradePointData;
        bakeryEntity.Get<BuyDataUpdateRequest>();
        bakeryEntity.Get<SellDataUpdateRequest>();

        var gasStationEntity = _world.NewEntity();
        gasStationEntity.Get<AutoService>();
        ref var gasStation = ref gasStationEntity.Get<ProductSeller>();
        gasStation.sellerGO = sceneData.gasStationTradePoint;
        gasStation.tradePointData = gasStation.sellerGO.GetComponent<TradePointData>();
        gasStation.produceSpeed = 0.3f;
        gasStation.repriceMultiplier = 1.1f;
        gasStation.product = new Product(ProductType.Fuel, productData.fuel, 1);
        gasStationEntity.Get<StorageComp>().maxMass = 200;
        gasStationEntity.Get<SellDataUpdateRequest>();

        var autoServiceEntity = _world.NewEntity();
        autoServiceEntity.Get<AutoService>();
        ref var autoService = ref autoServiceEntity.Get<ProductSeller>();
        autoService.sellerGO = sceneData.autoServiceTradePoint;
        autoService.tradePointData = autoService.sellerGO.GetComponent<TradePointData>();
        autoService.produceSpeed = 0.2f;
        autoService.repriceMultiplier = 1.1f;
        autoService.product = new Product(ProductType.AutoParts, productData.autoParts, 2);
        autoServiceEntity.Get<StorageComp>().maxMass = 200;
        autoServiceEntity.Get<SellDataUpdateRequest>();

        var shopEntity = _world.NewEntity();
        shopEntity.Get<Quest>().maxQuestTime = 5;
        ref var shopBuyer = ref shopEntity.Get<ProductBuyer>();
        shopBuyer.buyerGO = sceneData.shopTradePoint;
        shopBuyer.tradePointData = shopBuyer.buyerGO.GetComponent<TradePointData>();
        shopBuyer.repriceMultiplier = 1.2f;
        shopEntity.Get<StorageComp>().maxMass = 200;
        bakeryEntity.Get<BuyDataUpdateRequest>();



    }
}
