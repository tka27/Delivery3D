using Leopotam.Ecs;
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;


public class GameInitSystem : IEcsInitSystem
{
    EcsWorld _world;
    SceneData sceneData;
    BuildingsData buildingsData;
    StaticData staticData;
    SoundData soundData;
    PathData pathData;
    GameSettings settings;
    UIData uiData;
    ProductData productData;

    public void Init()
    {

        LoadForTests();
        staticData.currentMoney = staticData.moneyForGame;
        staticData.UpdateAvailableProducts();

        PlayerInit();
        BuildingsInit();
        LabInit();
        sceneData.navMeshSurface.BuildNavMesh();
        AnimalsInit();

    }

    void PlayerInit()
    {
        var playerEntity = _world.NewEntity();
        ref var playerComp = ref playerEntity.Get<Player>();
        playerEntity.Get<Movable>();
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
        playerComp.activeWheelColliders = new List<WheelCollider>();
        foreach (var wc in playerComp.carData.allWheelColliders)
        {
            if (wc.gameObject.activeInHierarchy)
            {
                playerComp.activeWheelColliders.Add(wc);
            }
        }
        foreach (var wheel in playerComp.carData.allWheelMeshes)
        {
            if (wheel.gameObject.activeInHierarchy)
            {
                playerComp.carData.wheelDatas.Add(wheel.GetComponent<WheelData>());
            }
        }

        soundData.loopSounds.Add(playerComp.carData.engineSound);
        soundData.SwitchLoopSounds(settings.sound);


        var virtualCam = sceneData.driveCam.GetComponent<CinemachineVirtualCamera>();
        virtualCam.Follow = playerComp.playerGO.transform;
        virtualCam.LookAt = playerComp.carData.cameraLookPoint;
    }

    void LabInit()
    {
        UpdateResearchList();
        if (staticData.researchLvl < sceneData.researchList.Count)
        {
            buildingsData.labTradePoint.SetActive(true);
            var labEntity = _world.NewEntity();
            ref var labComp = ref labEntity.Get<ResearchLab>();
            labComp.defaultRequirement = 10;
            ref var labBuyer = ref labEntity.Get<ProductBuyer>();
            labBuyer.buyingProductTypes = new List<ProductType>();
            labBuyer.buyerGO = buildingsData.labTradePoint;
            labBuyer.tradePointData = labBuyer.buyerGO.GetComponent<TradePointData>();
            labBuyer.repriceMultiplier = 1.2f;
            ref var labInventory = ref labEntity.Get<Inventory>();
            labInventory.inventory = new List<Product>();
            labInventory.maxMass = 50;
            labEntity.Get<LabUpdateRequest>();
            labEntity.Get<BuyDataUpdateRequest>();
            pathData.finalPoints.Add(labBuyer.tradePointData.finalPoint);
            buildingsData.tradePointCanvases.Add(labBuyer.tradePointData.canvas);
        }
        else
        {
            buildingsData.labTradePoint.SetActive(false);
        }
    }

    void BuildingsInit()
    {

        #region Farms

        #region Wheat

        var wheatFarmEntity = _world.NewEntity();
        ref var wheatFarmSeller = ref wheatFarmEntity.Get<ProductSeller>();
        wheatFarmSeller.sellerGO = buildingsData.wheatTradePoint;
        wheatFarmSeller.tradePointData = wheatFarmSeller.sellerGO.GetComponent<TradePointData>();
        wheatFarmSeller.produceSpeed = 0.5f * 100;
        wheatFarmSeller.product = new Product(ProductType.Wheat, productData.wheat, 0.1f);
        wheatFarmSeller.repriceMultiplier = 1.2f;
        ref var wheatFarmInventory = ref wheatFarmEntity.Get<Inventory>();
        wheatFarmInventory.inventory = new List<Product>();
        wheatFarmInventory.maxMass = 1000;
        wheatFarmEntity.Get<SellDataUpdateRequest>();
        pathData.finalPoints.Add(wheatFarmSeller.tradePointData.finalPoint);
        buildingsData.tradePointCanvases.Add(wheatFarmSeller.tradePointData.canvas);

        #endregion

        #region Fish

        var fishFarmEntity = _world.NewEntity();
        ref var fishFarmSeller = ref fishFarmEntity.Get<ProductSeller>();
        fishFarmSeller.sellerGO = buildingsData.fishTradePoint;
        fishFarmSeller.tradePointData = fishFarmSeller.sellerGO.GetComponent<TradePointData>();
        fishFarmSeller.produceSpeed = 0.5f;
        fishFarmSeller.product = new Product(ProductType.Fish, productData.fish, 0.5f);
        fishFarmSeller.repriceMultiplier = 1.2f;
        ref var fishFarmInventory = ref fishFarmEntity.Get<Inventory>();
        fishFarmInventory.inventory = new List<Product>();
        fishFarmInventory.maxMass = 50;
        fishFarmEntity.Get<SellDataUpdateRequest>();
        pathData.finalPoints.Add(fishFarmSeller.tradePointData.finalPoint);
        buildingsData.tradePointCanvases.Add(fishFarmSeller.tradePointData.canvas);

        #endregion

        #region Fruit

        var fruitFarmEntity = _world.NewEntity();
        ref var fruitFarmSeller = ref fruitFarmEntity.Get<ProductSeller>();
        fruitFarmSeller.sellerGO = buildingsData.fruitTradePoint;
        fruitFarmSeller.tradePointData = fruitFarmSeller.sellerGO.GetComponent<TradePointData>();
        fruitFarmSeller.produceSpeed = 0.5f;
        fruitFarmSeller.product = new Product(ProductType.Fruits, productData.fruits, 0.5f);
        fruitFarmSeller.repriceMultiplier = 1.2f;
        ref var fruitFarmInventory = ref fruitFarmEntity.Get<Inventory>();
        fruitFarmInventory.inventory = new List<Product>();
        fruitFarmInventory.maxMass = 50;
        fruitFarmEntity.Get<SellDataUpdateRequest>();
        pathData.finalPoints.Add(fruitFarmSeller.tradePointData.finalPoint);
        buildingsData.tradePointCanvases.Add(fruitFarmSeller.tradePointData.canvas);

        #endregion

        #region Vegetables

        var vegetableFarmEntity = _world.NewEntity();
        ref var vegetableFarmSeller = ref vegetableFarmEntity.Get<ProductSeller>();
        vegetableFarmSeller.sellerGO = buildingsData.vegetableTradePoint;
        vegetableFarmSeller.tradePointData = vegetableFarmSeller.sellerGO.GetComponent<TradePointData>();
        vegetableFarmSeller.produceSpeed = 0.5f;
        vegetableFarmSeller.product = new Product(ProductType.Vegetables, productData.vegetables, 0.5f);
        vegetableFarmSeller.repriceMultiplier = 1.2f;
        ref var vegetableFarmInventory = ref vegetableFarmEntity.Get<Inventory>();
        vegetableFarmInventory.inventory = new List<Product>();
        vegetableFarmInventory.maxMass = 50;
        vegetableFarmEntity.Get<SellDataUpdateRequest>();
        pathData.finalPoints.Add(vegetableFarmSeller.tradePointData.finalPoint);
        buildingsData.tradePointCanvases.Add(vegetableFarmSeller.tradePointData.canvas);

        #endregion

        #region Water

        var waterStationEntity = _world.NewEntity();
        ref var waterStationSeller = ref waterStationEntity.Get<ProductSeller>();
        waterStationSeller.sellerGO = buildingsData.waterTradePoint;
        waterStationSeller.tradePointData = waterStationSeller.sellerGO.GetComponent<TradePointData>();
        waterStationSeller.produceSpeed = 0.5f;
        waterStationSeller.product = new Product(ProductType.Water, productData.water, 0.5f);
        waterStationSeller.repriceMultiplier = 1.2f;
        ref var waterStationInventory = ref waterStationEntity.Get<Inventory>();
        waterStationInventory.inventory = new List<Product>();
        waterStationInventory.maxMass = 50;
        waterStationEntity.Get<SellDataUpdateRequest>();
        pathData.finalPoints.Add(waterStationSeller.tradePointData.finalPoint);
        buildingsData.tradePointCanvases.Add(waterStationSeller.tradePointData.canvas);

        #endregion

        #region Gas

        var gasStationEntity = _world.NewEntity();
        gasStationEntity.Get<AutoService>();
        ref var gasStation = ref gasStationEntity.Get<ProductSeller>();
        gasStation.sellerGO = buildingsData.gasStationTradePoint;
        gasStation.tradePointData = gasStation.sellerGO.GetComponent<TradePointData>();
        gasStation.produceSpeed = 0.3f;
        gasStation.repriceMultiplier = 1.1f;
        gasStation.product = new Product(ProductType.Fuel, productData.fuel, 1);
        ref var gasStationInventory = ref gasStationEntity.Get<Inventory>();
        gasStationInventory.inventory = new List<Product>();
        gasStationInventory.maxMass = 200;
        gasStationEntity.Get<SellDataUpdateRequest>();
        pathData.finalPoints.Add(gasStation.tradePointData.finalPoint);
        buildingsData.tradePointCanvases.Add(gasStation.tradePointData.canvas);

        #endregion

        #region Service

        var autoServiceEntity = _world.NewEntity();
        autoServiceEntity.Get<AutoService>();
        ref var autoService = ref autoServiceEntity.Get<ProductSeller>();
        autoService.sellerGO = buildingsData.autoServiceTradePoint;
        autoService.tradePointData = autoService.sellerGO.GetComponent<TradePointData>();
        autoService.produceSpeed = 0.2f;
        autoService.repriceMultiplier = 1.1f;
        autoService.product = new Product(ProductType.AutoParts, productData.autoParts, 2);
        ref var autoServiceInventory = ref autoServiceEntity.Get<Inventory>();
        autoServiceInventory.inventory = new List<Product>();
        autoServiceInventory.maxMass = 200;
        autoServiceEntity.Get<SellDataUpdateRequest>();
        pathData.finalPoints.Add(autoService.tradePointData.finalPoint);
        buildingsData.tradePointCanvases.Add(autoService.tradePointData.canvas);

        #endregion

        #endregion


        #region Factories

        #region Bakery

        var bakeryEntity = _world.NewEntity();
        ref var bakeryBuyer = ref bakeryEntity.Get<ProductBuyer>();
        bakeryBuyer.buyerGO = buildingsData.bakeryTradePoint;
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
        pathData.finalPoints.Add(bakeryBuyer.tradePointData.finalPoint);
        buildingsData.tradePointCanvases.Add(bakeryBuyer.tradePointData.canvas);


        #endregion

        #region Chicken
        var chickenEntity = _world.NewEntity();
        ref var chickenBuyer = ref chickenEntity.Get<ProductBuyer>();
        chickenBuyer.buyerGO = buildingsData.chickenTradePoint;
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
        pathData.finalPoints.Add(chickenBuyer.tradePointData.finalPoint);
        buildingsData.tradePointCanvases.Add(chickenBuyer.tradePointData.canvas);


        #endregion

        #region MeatFactory

        var meatEntity = _world.NewEntity();
        ref var meatBuyer = ref meatEntity.Get<ProductBuyer>();
        meatBuyer.buyerGO = buildingsData.meatFactoryTradePoint;
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
        pathData.finalPoints.Add(meatBuyer.tradePointData.finalPoint);
        buildingsData.tradePointCanvases.Add(meatBuyer.tradePointData.canvas);

        #endregion

        #region MilkFactory
        var milkEntity = _world.NewEntity();
        ref var milkBuyer = ref milkEntity.Get<ProductBuyer>();
        milkBuyer.buyerGO = buildingsData.milkFactoryTradePoint;
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
        pathData.finalPoints.Add(milkSeller.tradePointData.finalPoint);
        buildingsData.tradePointCanvases.Add(milkSeller.tradePointData.canvas);


        #endregion

        #region Pizza

        var pizzaEntity = _world.NewEntity();
        ref var pizzaBuyer = ref pizzaEntity.Get<ProductBuyer>();
        pizzaBuyer.buyerGO = buildingsData.pizzaTradePoint;
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
        pathData.finalPoints.Add(pizzaBuyer.tradePointData.finalPoint);
        buildingsData.tradePointCanvases.Add(pizzaBuyer.tradePointData.canvas);

        #endregion

        #region Cheese

        var cheeseEntity = _world.NewEntity();
        ref var cheeseBuyer = ref cheeseEntity.Get<ProductBuyer>();
        cheeseBuyer.buyerGO = buildingsData.cheeseTradePoint;
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
        pathData.finalPoints.Add(cheeseBuyer.tradePointData.finalPoint);
        buildingsData.tradePointCanvases.Add(cheeseBuyer.tradePointData.canvas);

        #endregion

        #region CanFish

        var canFishEntity = _world.NewEntity();
        ref var canFishBuyer = ref canFishEntity.Get<ProductBuyer>();
        canFishBuyer.buyerGO = buildingsData.canFishTradePoint;
        canFishBuyer.tradePointData = canFishBuyer.buyerGO.GetComponent<TradePointData>();
        canFishBuyer.repriceMultiplier = 1.2f;
        canFishBuyer.buyingProductTypes = new List<ProductType>();
        canFishBuyer.buyingProductTypes.Add(ProductType.Fish);

        ref var canFishInventory = ref canFishEntity.Get<Inventory>();
        canFishInventory.inventory = new List<Product>();
        canFishInventory.inventory.Add(new Product(ProductType.Fish, productData.fish, 0.75f));
        canFishInventory.maxMass = 50;

        ref var canFishSeller = ref canFishEntity.Get<ProductSeller>();
        canFishSeller.produceSpeed = 1;
        canFishSeller.sellerGO = canFishBuyer.buyerGO;
        canFishSeller.product = new Product(ProductType.CannedFish, productData.cannedFish, 1.33f);
        canFishInventory.inventory.Add(canFishSeller.product);
        canFishSeller.repriceMultiplier = 1.2f;
        canFishSeller.tradePointData = canFishBuyer.tradePointData;
        canFishEntity.Get<BuyDataUpdateRequest>();
        canFishEntity.Get<SellDataUpdateRequest>();
        pathData.finalPoints.Add(canFishBuyer.tradePointData.finalPoint);
        buildingsData.tradePointCanvases.Add(canFishBuyer.tradePointData.canvas);

        #endregion

        #region Juice

        var juiceEntity = _world.NewEntity();
        ref var juiceBuyer = ref juiceEntity.Get<ProductBuyer>();
        juiceBuyer.buyerGO = buildingsData.juiceTradePoint;
        juiceBuyer.tradePointData = juiceBuyer.buyerGO.GetComponent<TradePointData>();
        juiceBuyer.repriceMultiplier = 1.2f;
        juiceBuyer.buyingProductTypes = new List<ProductType>();
        juiceBuyer.buyingProductTypes.Add(ProductType.Water);
        juiceBuyer.buyingProductTypes.Add(ProductType.Fruits);

        ref var juiceInventory = ref juiceEntity.Get<Inventory>();
        juiceInventory.inventory = new List<Product>();
        juiceInventory.inventory.Add(new Product(ProductType.Water, productData.water, 0.6f));
        juiceInventory.inventory.Add(new Product(ProductType.Fruits, productData.fruits, 0.6f));
        juiceInventory.maxMass = 100;

        ref var juiceSeller = ref juiceEntity.Get<ProductSeller>();
        juiceSeller.product = new Product(ProductType.Juice, productData.juice, 1.33f);
        juiceInventory.inventory.Add(juiceSeller.product);
        juiceSeller.produceSpeed = 1;
        juiceSeller.sellerGO = juiceBuyer.buyerGO;
        juiceSeller.repriceMultiplier = 1.2f;
        juiceSeller.tradePointData = juiceBuyer.tradePointData;
        juiceEntity.Get<BuyDataUpdateRequest>();
        juiceEntity.Get<SellDataUpdateRequest>();
        pathData.finalPoints.Add(juiceBuyer.tradePointData.finalPoint);
        buildingsData.tradePointCanvases.Add(juiceBuyer.tradePointData.canvas);

        #endregion

        #region IceCream

        var iceEntity = _world.NewEntity();
        ref var iceBuyer = ref iceEntity.Get<ProductBuyer>();
        iceBuyer.buyerGO = buildingsData.iceTradePoint;
        iceBuyer.tradePointData = iceBuyer.buyerGO.GetComponent<TradePointData>();
        iceBuyer.repriceMultiplier = 1.2f;
        iceBuyer.buyingProductTypes = new List<ProductType>();
        iceBuyer.buyingProductTypes.Add(ProductType.Water);
        iceBuyer.buyingProductTypes.Add(ProductType.Fruits);

        ref var iceInventory = ref iceEntity.Get<Inventory>();
        iceInventory.inventory = new List<Product>();
        iceInventory.inventory.Add(new Product(ProductType.Milk, productData.milk, 0.6f));
        iceInventory.inventory.Add(new Product(ProductType.Fruits, productData.fruits, 0.6f));
        iceInventory.maxMass = 100;

        ref var iceSeller = ref iceEntity.Get<ProductSeller>();
        iceSeller.product = new Product(ProductType.Juice, productData.iceCream, 1.33f);
        iceInventory.inventory.Add(iceSeller.product);
        iceSeller.produceSpeed = 1;
        iceSeller.sellerGO = iceBuyer.buyerGO;
        iceSeller.repriceMultiplier = 1.2f;
        iceSeller.tradePointData = iceBuyer.tradePointData;
        iceEntity.Get<BuyDataUpdateRequest>();
        iceEntity.Get<SellDataUpdateRequest>();
        pathData.finalPoints.Add(iceBuyer.tradePointData.finalPoint);
        buildingsData.tradePointCanvases.Add(iceBuyer.tradePointData.canvas);

        #endregion

        //factories end
        #endregion


        var shopEntity = _world.NewEntity();
        shopEntity.Get<Quest>().maxQuestTime = 120;
        ref var shopBuyer = ref shopEntity.Get<ProductBuyer>();
        shopBuyer.buyingProductTypes = new List<ProductType>();
        shopBuyer.buyerGO = buildingsData.shopTradePoint;
        shopBuyer.tradePointData = shopBuyer.buyerGO.GetComponent<TradePointData>();
        shopBuyer.repriceMultiplier = 1.2f;
        ref var shopInventory = ref shopEntity.Get<Inventory>();
        shopInventory.inventory = new List<Product>();
        shopInventory.maxMass = 50;
        shopEntity.Get<BuyDataUpdateRequest>();
        pathData.finalPoints.Add(shopBuyer.tradePointData.finalPoint);
        buildingsData.tradePointCanvases.Add(shopBuyer.tradePointData.canvas);
    }



    void UpdateResearchList()
    {
        sceneData.researchList.Add(new Product(ProductType.Wheat, productData.wheat, -0.1f));
        sceneData.researchList.Add(new Product(ProductType.Water, productData.water, -0.1f));
        sceneData.researchList.Add(new Product(ProductType.Meat, productData.meat, -0.1f));
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
            int randomIndex = Random.Range(0, sceneData.animalSpawnPoints.Count);
            animal.transform.position = sceneData.animalSpawnPoints[randomIndex].position;
        }
    }
}
