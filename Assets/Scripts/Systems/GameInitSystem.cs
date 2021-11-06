using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameInitSystem : IEcsInitSystem
{
    EcsWorld _world;
    SceneData sceneData;
    UIData uiData;
    ProductData productData;
    public void Init()
    {
        sceneData.money = 100;
        uiData.moneyText.text = sceneData.money.ToString("0");
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
        playerComp.maxDurability = 100;
        playerComp.currentDurability = playerComp.maxDurability;
        uiData.durabilityText.text = playerComp.currentDurability.ToString();
        playerComp.maxFuel = 100;
        playerComp.currentFuel = playerComp.maxFuel;
        uiData.fuelText.text = playerComp.currentFuel.ToString();
        playerComp.fuelConsumption = 0.01f;
        playerEntity.Get<CargoComp>().inventory = new List<Product>();
        ref var playerStorage = ref playerEntity.Get<StorageComp>();
        playerStorage.maxMass = 50;
        uiData.cargoText.text = playerStorage.currentMass + "/" + playerStorage.maxMass;



        var wheatFarmEntity = _world.NewEntity();
        ref var wheatFarm = ref wheatFarmEntity.Get<ProductSeller>();
        wheatFarm.sellerGO = sceneData.wheatFarmTradePoint;
        wheatFarm.tradePointData = wheatFarm.sellerGO.GetComponent<TradePointData>();
        wheatFarm.produceSpeed = 2;
        wheatFarm.sellingProduct = new Product(ProductType.Wheat, productData.wheat);
        wheatFarm.sellPrice = 0.5f;
        wheatFarm.tradePointData.sellPrice.text = wheatFarm.sellPrice.ToString("0.00");
        wheatFarmEntity.Get<StorageComp>().maxMass = 200;

        var bakeryEntity = _world.NewEntity();
        ref var bakeryBuyer = ref bakeryEntity.Get<ProductBuyer>();
        bakeryBuyer.buyerGO = sceneData.bakeryTradePoint;
        bakeryBuyer.tradePointData = bakeryBuyer.buyerGO.GetComponent<TradePointData>();
        bakeryBuyer.buyingProduct = new Product(ProductType.Wheat, productData.wheat);
        bakeryBuyer.buyPrice = 1;
        bakeryBuyer.tradePointData.buyPrice.text = bakeryBuyer.buyPrice.ToString("0.00");
        bakeryEntity.Get<StorageComp>().maxMass = 200;
        ref var bakerySeller = ref bakeryEntity.Get<ProductSeller>();
        bakerySeller.produceSpeed = 1;
        bakerySeller.sellerGO = bakeryBuyer.buyerGO;
        bakerySeller.sellingProduct = new Product(ProductType.Wheat, productData.bread);
        bakerySeller.sellPrice = 1.5f;
        bakerySeller.tradePointData = bakeryBuyer.tradePointData;
        bakerySeller.tradePointData.sellPrice.text = bakerySeller.sellPrice.ToString("0.00");

        var GasStationEntity = _world.NewEntity();
        GasStationEntity.Get<AutoService>();
        ref var gasStation = ref GasStationEntity.Get<ProductSeller>();
        gasStation.sellerGO = sceneData.gasStationTradePoint;
        gasStation.tradePointData = gasStation.sellerGO.GetComponent<TradePointData>();
        gasStation.produceSpeed = 1;
        gasStation.sellingProduct = new Product(ProductType.Fuel, productData.fuel);
        gasStation.sellPrice = 1f;
        gasStation.tradePointData.sellPrice.text = gasStation.sellPrice.ToString("0.00");
        GasStationEntity.Get<StorageComp>().maxMass = 200;




    }
}
