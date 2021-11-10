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
        ref var playerInventory = ref playerEntity.Get<Inventory>();
        playerInventory.inventory = new List<Product>();
        playerInventory.maxMass = playerComp.carData.maxStorageMass;// 50;
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




        #region Farms

        #region Wheat

        var wheatFarmEntity = _world.NewEntity();
        ref var wheatFarm = ref wheatFarmEntity.Get<ProductSeller>();
        wheatFarm.sellerGO = sceneData.wheatFarmTradePoint;
        wheatFarm.tradePointData = wheatFarm.sellerGO.GetComponent<TradePointData>();
        wheatFarm.produceSpeed = 0.5f * 10;
        wheatFarm.product = new Product(ProductType.Wheat, productData.wheat, 0.5f);
        wheatFarm.repriceMultiplier = 1.2f;
        ref var wheatFarmInventory = ref wheatFarmEntity.Get<Inventory>();
        wheatFarmInventory.inventory = new List<Product>();
        wheatFarmInventory.maxMass = 200;
        wheatFarmEntity.Get<SellDataUpdateRequest>();

        #endregion

        #region Gas

        var gasStationEntity = _world.NewEntity();
        gasStationEntity.Get<AutoService>();
        ref var gasStation = ref gasStationEntity.Get<ProductSeller>();
        gasStation.sellerGO = sceneData.gasStationTradePoint;
        gasStation.tradePointData = gasStation.sellerGO.GetComponent<TradePointData>();
        gasStation.produceSpeed = 0.3f;
        gasStation.repriceMultiplier = 1.1f;
        gasStation.product = new Product(ProductType.Fuel, productData.fuel, 1);
        ref var gasStationInventory = ref gasStationEntity.Get<Inventory>();
        gasStationInventory.inventory = new List<Product>();
        gasStationInventory.maxMass = 200;
        gasStationEntity.Get<SellDataUpdateRequest>();

        #endregion

        #region Service

        var autoServiceEntity = _world.NewEntity();
        autoServiceEntity.Get<AutoService>();
        ref var autoService = ref autoServiceEntity.Get<ProductSeller>();
        autoService.sellerGO = sceneData.autoServiceTradePoint;
        autoService.tradePointData = autoService.sellerGO.GetComponent<TradePointData>();
        autoService.produceSpeed = 0.2f;
        autoService.repriceMultiplier = 1.1f;
        autoService.product = new Product(ProductType.AutoParts, productData.autoParts, 2);
        ref var autoServiceInventory = ref autoServiceEntity.Get<Inventory>();
        autoServiceInventory.inventory = new List<Product>();
        autoServiceInventory.maxMass = 200;
        autoServiceEntity.Get<SellDataUpdateRequest>();

        #endregion

        #endregion


        #region Factories

        #region Bakery
        var bakeryEntity = _world.NewEntity();
        ref var bakeryBuyer = ref bakeryEntity.Get<ProductBuyer>();
        bakeryBuyer.buyerGO = sceneData.bakeryTradePoint;
        bakeryBuyer.tradePointData = bakeryBuyer.buyerGO.GetComponent<TradePointData>();
        bakeryBuyer.repriceMultiplier = 1.2f;
        bakeryBuyer.buyingProductTypes = new List<ProductType>();
        bakeryBuyer.buyingProductTypes.Add(ProductType.Wheat);

        ref var bakeryInventory = ref bakeryEntity.Get<Inventory>();
        bakeryInventory.inventory = new List<Product>();
        bakeryInventory.inventory.Add(new Product(ProductType.Wheat, productData.wheat, 0.75f));
        bakeryInventory.maxMass = 20;

        ref var bakerySeller = ref bakeryEntity.Get<ProductSeller>();
        bakerySeller.produceSpeed = 1;
        bakerySeller.sellerGO = bakeryBuyer.buyerGO;
        bakerySeller.product = new Product(ProductType.Bread, productData.bread, 1.33f);
        bakeryInventory.inventory.Add(bakerySeller.product);
        bakerySeller.repriceMultiplier = 1.2f;
        bakerySeller.tradePointData = bakeryBuyer.tradePointData;
        bakeryEntity.Get<BuyDataUpdateRequest>();
        bakeryEntity.Get<SellDataUpdateRequest>();
        #endregion

        #region MeatFactory

        var meatEntity = _world.NewEntity();
        ref var meatBuyer = ref meatEntity.Get<ProductBuyer>();
        meatBuyer.buyerGO = sceneData.meatFactoryTradePoint;
        meatBuyer.tradePointData = meatBuyer.buyerGO.GetComponent<TradePointData>();
        meatBuyer.repriceMultiplier = 1.2f;
        meatBuyer.buyingProductTypes = new List<ProductType>();
        meatBuyer.buyingProductTypes.Add(ProductType.Wheat);

        ref var meatInventory = ref meatEntity.Get<Inventory>();
        meatInventory.inventory = new List<Product>();
        meatInventory.inventory.Add(new Product(ProductType.Wheat, productData.wheat, 0.6f));
        meatInventory.maxMass = 20;

        ref var meatSeller = ref meatEntity.Get<ProductSeller>();
        meatSeller.product = new Product(ProductType.Meat, productData.meat, 1.33f);
        meatInventory.inventory.Add(meatSeller.product);
        meatSeller.produceSpeed = 1;
        meatSeller.sellerGO = meatBuyer.buyerGO;
        meatSeller.repriceMultiplier = 1.2f;
        meatSeller.tradePointData = meatBuyer.tradePointData;
        meatEntity.Get<BuyDataUpdateRequest>();
        meatEntity.Get<SellDataUpdateRequest>();

        #endregion

        #region MilkFactory
        var milkEntity = _world.NewEntity();
        ref var milkBuyer = ref milkEntity.Get<ProductBuyer>();
        milkBuyer.buyerGO = sceneData.milkFactoryTradePoint;
        milkBuyer.tradePointData = milkBuyer.buyerGO.GetComponent<TradePointData>();
        milkBuyer.repriceMultiplier = 1.2f;
        milkBuyer.buyingProductTypes = new List<ProductType>();
        milkBuyer.buyingProductTypes.Add(ProductType.Wheat);

        ref var milkInventory = ref milkEntity.Get<Inventory>();
        milkInventory.inventory = new List<Product>();
        milkInventory.inventory.Add(new Product(ProductType.Wheat, productData.milk, 0.6f));
        milkInventory.maxMass = 20;

        ref var milkSeller = ref milkEntity.Get<ProductSeller>();
        milkSeller.product = new Product(ProductType.Milk, productData.milk, 1.33f);
        milkInventory.inventory.Add(milkSeller.product);
        milkSeller.produceSpeed = 1;
        milkSeller.sellerGO = milkBuyer.buyerGO;
        milkSeller.repriceMultiplier = 1.2f;
        milkSeller.tradePointData = milkBuyer.tradePointData;
        milkEntity.Get<BuyDataUpdateRequest>();
        milkEntity.Get<SellDataUpdateRequest>();

        #endregion

        #region Pizza

        var pizzaEntity = _world.NewEntity();
        ref var pizzaBuyer = ref pizzaEntity.Get<ProductBuyer>();
        pizzaBuyer.buyerGO = sceneData.pizzaTradePoint;
        pizzaBuyer.tradePointData = pizzaBuyer.buyerGO.GetComponent<TradePointData>();
        pizzaBuyer.repriceMultiplier = 1.2f;
        pizzaBuyer.buyingProductTypes = new List<ProductType>();
        pizzaBuyer.buyingProductTypes.Add(ProductType.Bread);
        pizzaBuyer.buyingProductTypes.Add(ProductType.Meat);
        pizzaBuyer.buyingProductTypes.Add(ProductType.Cheese);

        ref var pizzaInventory = ref pizzaEntity.Get<Inventory>();
        pizzaInventory.inventory = new List<Product>();
        pizzaInventory.inventory.Add(new Product(ProductType.Bread, productData.bread, 0.6f));
        pizzaInventory.inventory.Add(new Product(ProductType.Meat, productData.meat, 0.6f));
        pizzaInventory.inventory.Add(new Product(ProductType.Cheese, productData.cheese, 0.6f));
        pizzaInventory.maxMass = 20;

        ref var pizzaSeller = ref pizzaEntity.Get<ProductSeller>();
        pizzaSeller.product = new Product(ProductType.Pizza, productData.pizza, 1.33f);
        pizzaInventory.inventory.Add(pizzaSeller.product);
        pizzaSeller.produceSpeed = 1;
        pizzaSeller.sellerGO = pizzaBuyer.buyerGO;
        pizzaSeller.repriceMultiplier = 1.2f;
        pizzaSeller.tradePointData = pizzaBuyer.tradePointData;
        pizzaEntity.Get<BuyDataUpdateRequest>();
        pizzaEntity.Get<SellDataUpdateRequest>();

        #endregion

        #region Cheese

        var cheeseEntity = _world.NewEntity();
        ref var cheeseBuyer = ref cheeseEntity.Get<ProductBuyer>();
        cheeseBuyer.buyerGO = sceneData.cheeseTradePoint;
        cheeseBuyer.tradePointData = cheeseBuyer.buyerGO.GetComponent<TradePointData>();
        cheeseBuyer.repriceMultiplier = 1.2f;
        cheeseBuyer.buyingProductTypes = new List<ProductType>();
        cheeseBuyer.buyingProductTypes.Add(ProductType.Milk);

        ref var cheeseInventory = ref cheeseEntity.Get<Inventory>();
        cheeseInventory.inventory = new List<Product>();
        cheeseInventory.inventory.Add(new Product(ProductType.Milk, productData.milk, 0.6f));
        cheeseInventory.maxMass = 20;

        ref var cheeseSeller = ref cheeseEntity.Get<ProductSeller>();
        cheeseSeller.product = new Product(ProductType.Cheese, productData.cheese, 1.33f);
        cheeseInventory.inventory.Add(cheeseSeller.product);
        cheeseSeller.produceSpeed = 1;
        cheeseSeller.sellerGO = cheeseBuyer.buyerGO;
        cheeseSeller.repriceMultiplier = 1.2f;
        cheeseSeller.tradePointData = cheeseBuyer.tradePointData;
        cheeseEntity.Get<BuyDataUpdateRequest>();
        cheeseEntity.Get<SellDataUpdateRequest>();

        #endregion



        //factories end
        #endregion


        var shopEntity = _world.NewEntity();
        shopEntity.Get<Quest>().maxQuestTime = 3;
        ref var shopBuyer = ref shopEntity.Get<ProductBuyer>();
        shopBuyer.buyingProductTypes = new List<ProductType>();
        shopBuyer.buyerGO = sceneData.shopTradePoint;
        shopBuyer.tradePointData = shopBuyer.buyerGO.GetComponent<TradePointData>();
        shopBuyer.repriceMultiplier = 1.2f;
        ref var shopInventory = ref shopEntity.Get<Inventory>();
        shopInventory.inventory = new List<Product>();
        shopInventory.maxMass = 50;
        bakeryEntity.Get<BuyDataUpdateRequest>();



    }
}
