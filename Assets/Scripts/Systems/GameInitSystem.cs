using Leopotam.Ecs;
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;


public class GameInitSystem : IEcsInitSystem
{
    EcsWorld _world;
    SceneData sceneData;
    StaticData staticData;
    SoundData soundData;
    GameSettings settings;
    UIData uiData;
    ProductData productData;
    public void Init()
    {

        LoadForTests();
        staticData.currentMoney = staticData.moneyForGame;
        staticData.UpdateAvailableProducts();

        PlayerInit();
        AnimalsInit();
        LabInit();


        var pathEntity = _world.NewEntity();
        ref var pathComp = ref pathEntity.Get<PathComp>();
        pathComp.wayPoints = new List<GameObject>();
        pathComp.lineRenderer = sceneData.lineRenderer;
        pathComp.waypointsPool = sceneData.waypointsPool;




        #region Farms

        #region Wheat

        var wheatFarmEntity = _world.NewEntity();
        ref var wheatFarmSeller = ref wheatFarmEntity.Get<ProductSeller>();
        wheatFarmSeller.sellerGO = sceneData.wheatFarmTradePoint;
        wheatFarmSeller.tradePointData = wheatFarmSeller.sellerGO.GetComponent<TradePointData>();
        wheatFarmSeller.produceSpeed = 0.5f;
        wheatFarmSeller.product = new Product(ProductType.Wheat, productData.wheat, 0.5f);
        wheatFarmSeller.repriceMultiplier = 1.2f;
        ref var wheatFarmInventory = ref wheatFarmEntity.Get<Inventory>();
        wheatFarmInventory.inventory = new List<Product>();
        wheatFarmInventory.maxMass = 50;
        wheatFarmEntity.Get<SellDataUpdateRequest>();
        sceneData.finalPoints.Add(wheatFarmSeller.tradePointData.finalPoint);
        sceneData.tradePointCanvases.Add(wheatFarmSeller.tradePointData.canvas);

        #endregion

        #region Water

        var waterStationEntity = _world.NewEntity();
        ref var waterStationSeller = ref waterStationEntity.Get<ProductSeller>();
        waterStationSeller.sellerGO = sceneData.waterStationTradePoint;
        waterStationSeller.tradePointData = waterStationSeller.sellerGO.GetComponent<TradePointData>();
        waterStationSeller.produceSpeed = 0.5f;
        waterStationSeller.product = new Product(ProductType.Water, productData.water, 0.5f);
        waterStationSeller.repriceMultiplier = 1.2f;
        ref var waterStationInventory = ref waterStationEntity.Get<Inventory>();
        waterStationInventory.inventory = new List<Product>();
        waterStationInventory.maxMass = 50;
        waterStationEntity.Get<SellDataUpdateRequest>();
        sceneData.finalPoints.Add(waterStationSeller.tradePointData.finalPoint);
        sceneData.tradePointCanvases.Add(waterStationSeller.tradePointData.canvas);

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
        sceneData.finalPoints.Add(gasStation.tradePointData.finalPoint);
        sceneData.tradePointCanvases.Add(gasStation.tradePointData.canvas);

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
        sceneData.finalPoints.Add(autoService.tradePointData.finalPoint);
        sceneData.tradePointCanvases.Add(autoService.tradePointData.canvas);

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
        bakeryInventory.maxMass = 50;

        ref var bakerySeller = ref bakeryEntity.Get<ProductSeller>();
        bakerySeller.produceSpeed = 1;
        bakerySeller.sellerGO = bakeryBuyer.buyerGO;
        bakerySeller.product = new Product(ProductType.Bread, productData.bread, 1.33f);
        bakeryInventory.inventory.Add(bakerySeller.product);
        bakerySeller.repriceMultiplier = 1.2f;
        bakerySeller.tradePointData = bakeryBuyer.tradePointData;
        bakeryEntity.Get<BuyDataUpdateRequest>();
        bakeryEntity.Get<SellDataUpdateRequest>();
        sceneData.finalPoints.Add(bakeryBuyer.tradePointData.finalPoint);
        sceneData.tradePointCanvases.Add(bakeryBuyer.tradePointData.canvas);


        #endregion

        #region Chicken
        var chickenEntity = _world.NewEntity();
        ref var chickenBuyer = ref chickenEntity.Get<ProductBuyer>();
        chickenBuyer.buyerGO = sceneData.chickenTradePoint;
        chickenBuyer.tradePointData = chickenBuyer.buyerGO.GetComponent<TradePointData>();
        chickenBuyer.repriceMultiplier = 1.2f;
        chickenBuyer.buyingProductTypes = new List<ProductType>();
        chickenBuyer.buyingProductTypes.Add(ProductType.Wheat);

        ref var chickenInventory = ref chickenEntity.Get<Inventory>();
        chickenInventory.inventory = new List<Product>();
        chickenInventory.inventory.Add(new Product(ProductType.Wheat, productData.wheat, 0.75f));
        chickenInventory.maxMass = 50;

        ref var chickenSeller = ref chickenEntity.Get<ProductSeller>();
        chickenSeller.produceSpeed = 1;
        chickenSeller.sellerGO = chickenBuyer.buyerGO;
        chickenSeller.product = new Product(ProductType.Eggs, productData.eggs, 1.33f);
        chickenInventory.inventory.Add(chickenSeller.product);
        chickenSeller.repriceMultiplier = 1.2f;
        chickenSeller.tradePointData = chickenBuyer.tradePointData;
        chickenEntity.Get<BuyDataUpdateRequest>();
        chickenEntity.Get<SellDataUpdateRequest>();
        sceneData.finalPoints.Add(chickenBuyer.tradePointData.finalPoint);
        sceneData.tradePointCanvases.Add(chickenBuyer.tradePointData.canvas);


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
        meatInventory.maxMass = 50;

        ref var meatSeller = ref meatEntity.Get<ProductSeller>();
        meatSeller.product = new Product(ProductType.Meat, productData.meat, 1.33f);
        meatInventory.inventory.Add(meatSeller.product);
        meatSeller.produceSpeed = 1;
        meatSeller.sellerGO = meatBuyer.buyerGO;
        meatSeller.repriceMultiplier = 1.2f;
        meatSeller.tradePointData = meatBuyer.tradePointData;
        meatEntity.Get<BuyDataUpdateRequest>();
        meatEntity.Get<SellDataUpdateRequest>();
        sceneData.finalPoints.Add(meatBuyer.tradePointData.finalPoint);
        sceneData.tradePointCanvases.Add(meatBuyer.tradePointData.canvas);

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
        milkInventory.maxMass = 50;

        ref var milkSeller = ref milkEntity.Get<ProductSeller>();
        milkSeller.product = new Product(ProductType.Milk, productData.milk, 1.33f);
        milkInventory.inventory.Add(milkSeller.product);
        milkSeller.produceSpeed = 1;
        milkSeller.sellerGO = milkBuyer.buyerGO;
        milkSeller.repriceMultiplier = 1.2f;
        milkSeller.tradePointData = milkBuyer.tradePointData;
        milkEntity.Get<BuyDataUpdateRequest>();
        milkEntity.Get<SellDataUpdateRequest>();
        sceneData.finalPoints.Add(milkSeller.tradePointData.finalPoint);
        sceneData.tradePointCanvases.Add(milkSeller.tradePointData.canvas);


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
        pizzaInventory.maxMass = 50;

        ref var pizzaSeller = ref pizzaEntity.Get<ProductSeller>();
        pizzaSeller.product = new Product(ProductType.Pizza, productData.pizza, 1.33f);
        pizzaInventory.inventory.Add(pizzaSeller.product);
        pizzaSeller.produceSpeed = 1;
        pizzaSeller.sellerGO = pizzaBuyer.buyerGO;
        pizzaSeller.repriceMultiplier = 1.2f;
        pizzaSeller.tradePointData = pizzaBuyer.tradePointData;
        pizzaEntity.Get<BuyDataUpdateRequest>();
        pizzaEntity.Get<SellDataUpdateRequest>();
        sceneData.finalPoints.Add(pizzaBuyer.tradePointData.finalPoint);
        sceneData.tradePointCanvases.Add(pizzaBuyer.tradePointData.canvas);

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
        cheeseInventory.maxMass = 50;

        ref var cheeseSeller = ref cheeseEntity.Get<ProductSeller>();
        cheeseSeller.product = new Product(ProductType.Cheese, productData.cheese, 1.33f);
        cheeseInventory.inventory.Add(cheeseSeller.product);
        cheeseSeller.produceSpeed = 1;
        cheeseSeller.sellerGO = cheeseBuyer.buyerGO;
        cheeseSeller.repriceMultiplier = 1.2f;
        cheeseSeller.tradePointData = cheeseBuyer.tradePointData;
        cheeseEntity.Get<BuyDataUpdateRequest>();
        cheeseEntity.Get<SellDataUpdateRequest>();
        sceneData.finalPoints.Add(cheeseBuyer.tradePointData.finalPoint);
        sceneData.tradePointCanvases.Add(cheeseBuyer.tradePointData.canvas);

        #endregion



        //factories end
        #endregion


        var shopEntity = _world.NewEntity();
        shopEntity.Get<Quest>().maxQuestTime = 120;
        ref var shopBuyer = ref shopEntity.Get<ProductBuyer>();
        shopBuyer.buyingProductTypes = new List<ProductType>();
        shopBuyer.buyerGO = sceneData.shopTradePoint;
        shopBuyer.tradePointData = shopBuyer.buyerGO.GetComponent<TradePointData>();
        shopBuyer.repriceMultiplier = 1.2f;
        ref var shopInventory = ref shopEntity.Get<Inventory>();
        shopInventory.inventory = new List<Product>();
        shopInventory.maxMass = 50;
        shopEntity.Get<BuyDataUpdateRequest>();
        sceneData.finalPoints.Add(shopBuyer.tradePointData.finalPoint);
        sceneData.tradePointCanvases.Add(shopBuyer.tradePointData.canvas);





    }

    void PlayerInit()
    {
        var playerEntity = _world.NewEntity();
        ref var playerComp = ref playerEntity.Get<Player>();
        playerEntity.Get<MovableComp>();
        sceneData.cars[staticData.selectedCarID].SetActive(true);
        playerComp.playerGO = sceneData.cars[staticData.selectedCarID];
        playerComp.carData = playerComp.playerGO.GetComponent<CarData>();
        playerComp.playerRB = playerComp.playerGO.GetComponent<Rigidbody>();
        playerComp.playerRB.mass = playerComp.carData.defaultMass;
        playerComp.playerRB.centerOfMass = playerComp.carData.centerOfMass.transform.localPosition;
        playerComp.maxSteerAngle = playerComp.carData.maxSteerAngle;
        playerComp.maxFuel = playerComp.carData.maxFuel + playerComp.carData.maxFuel / 100 * 5 * staticData.carPerks[staticData.selectedCarID][0];
        playerComp.maxTorque = playerComp.carData.maxTorque + playerComp.carData.maxTorque / 100 * 5 * staticData.carPerks[staticData.selectedCarID][1];
        playerComp.acceleration = playerComp.carData.acceleration + playerComp.carData.acceleration / 100 * 5 * staticData.carPerks[staticData.selectedCarID][2];
        playerComp.maxDurability = playerComp.carData.maxDurability + playerComp.carData.maxDurability / 100 * 5 * staticData.carPerks[staticData.selectedCarID][3];
        playerComp.currentDurability = playerComp.maxDurability;
        uiData.durabilityText.text = playerComp.currentDurability.ToString();
        playerComp.currentFuel = playerComp.maxFuel;
        uiData.fuelText.text = playerComp.currentFuel.ToString();
        ref var playerInventory = ref playerEntity.Get<Inventory>();
        playerInventory.inventory = new List<Product>();
        if (!staticData.trailerIsSelected)
        {
            playerInventory.maxMass = playerComp.carData.carStorage + playerComp.carData.carStorage / 100 * 5 * staticData.carPerks[staticData.selectedCarID][4];
        }
        else
        {
            playerInventory.maxMass = playerComp.carData.carStorage + playerComp.carData.trailerStorage + playerComp.carData.carStorage / 100 * 5 * staticData.carPerks[staticData.selectedCarID][4];
        }

        playerEntity.Get<UpdateCargoRequest>();

        foreach (var wheel in playerComp.carData.allWheelMeshes)
        {
            playerComp.carData.wheelDatas.Add(wheel.GetComponent<WheelData>());
        }

        for (int i = 0; i < playerComp.carData.playerCargo.Count; i++)
        {
            playerComp.carData.playerCargoRB.Add(playerComp.carData.playerCargo[i].gameObject.GetComponent<Rigidbody>());
            playerComp.carData.playerCargoDefaultPos.Add(playerComp.carData.playerCargo[i].transform.localPosition);
            playerComp.carData.playerCargoDefaultRot.Add(playerComp.carData.playerCargo[i].transform.localRotation);
        }
        if (staticData.trailerIsSelected)
        {
            playerComp.carData.trailer.SetActive(true);
        }
        else
        {
            playerComp.carData.trailer.SetActive(false);
        }

        soundData.loopSounds.Add(playerComp.carData.engineSound);
        soundData.SwitchLoopSounds(settings.sound);

        var virtualCam = sceneData.driveCam.GetComponent<CinemachineVirtualCamera>();
        virtualCam.Follow = playerComp.playerGO.transform;
        virtualCam.LookAt = playerComp.carData.cameraLookPoint;
    }


    void LabInit()
    {
        if (staticData.researchLvl < sceneData.researchList.Count)
        {
            UpdateResearchList();

            sceneData.labTradePoint.SetActive(true);
            var labEntity = _world.NewEntity();
            ref var labComp = ref labEntity.Get<ResearchLab>();
            labComp.requirement = 50;
            ref var labBuyer = ref labEntity.Get<ProductBuyer>();
            labBuyer.buyingProductTypes = new List<ProductType>();
            labBuyer.buyingProductTypes.Add(sceneData.researchList[staticData.researchLvl].type);
            labBuyer.buyerGO = sceneData.labTradePoint;
            labBuyer.tradePointData = labBuyer.buyerGO.GetComponent<TradePointData>();
            labBuyer.repriceMultiplier = 1.2f;
            labBuyer.tradePointData.labProgress.text = labComp.progress.ToString() + "/" + labComp.requirement.ToString();
            ref var labInventory = ref labEntity.Get<Inventory>();
            labInventory.inventory = new List<Product>();
            labInventory.inventory.Add(sceneData.researchList[staticData.researchLvl]);
            labBuyer.tradePointData.buyProductSpriteRenderer.sprite = labInventory.inventory[staticData.researchLvl].icon;
            labInventory.maxMass = 50;
            labEntity.Get<BuyDataUpdateRequest>();
            sceneData.finalPoints.Add(labBuyer.tradePointData.finalPoint);
            sceneData.tradePointCanvases.Add(labBuyer.tradePointData.canvas);
        }
        else
        {
            sceneData.labTradePoint.SetActive(false);
        }
    }

    void UpdateResearchList()
    {
        sceneData.researchList.Add(new Product(ProductType.Wheat, productData.wheat, -0.1f));
    }


    void LoadForTests()
    {
        settings.LoadPrefs();
        staticData.carPerks = new int[sceneData.cars.Count][];
        for (int i = 0; i < sceneData.cars.Count; i++)
        {
            staticData.carPerks[i] = new int[5];
        }
        LoadGameProgress();
    }


    void LoadGameProgress() //copy SaveData to staticData
    {
        SaveData data = SaveSystem.Load();
        if (data != null)
        {
            staticData.UpdateStaticData(data);
        }
    }


    void AnimalsInit()
    {
        foreach (var animal in sceneData.animalsPool)
        {
            _world.NewEntity().Get<Animal>().animalData = animal.GetComponent<AnimalData>();
            int randomIndex = Random.Range(0,sceneData.animalSpawnPoints.Count);
            animal.transform.position = sceneData.animalSpawnPoints[randomIndex].position;
        }
    }
}
