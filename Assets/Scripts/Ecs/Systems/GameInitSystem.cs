using Leopotam.Ecs;
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;
using Firebase.Analytics;

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
        
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart);

        if (Application.isEditor) LoadForTests();


        staticData.currentMoney = staticData.moneyForGame;
        staticData.totalMoney -= staticData.moneyForGame;
        staticData.UpdateAvailableProducts();

        PlayerInit();
        LabInit();
        AnimalsInit();

    }

    void PlayerInit()
    {
        var playerEntity = _world.NewEntity();
        ref var player = ref playerEntity.Get<Player>();
        playerEntity.Get<Movable>();
        sceneData.cars[staticData.selectedCarID].SetActive(true);
        player.playerGO = sceneData.cars[staticData.selectedCarID];
        player.carData = player.playerGO.GetComponent<CarData>();
        player.playerRB = player.playerGO.GetComponent<Rigidbody>();
        player.playerRB.mass = player.carData.defaultMass;
        player.playerRB.centerOfMass = player.carData.centerOfMass.transform.localPosition;
        player.maxSteerAngle = player.carData.maxSteerAngle;
        player.maxSpeed = player.carData.maxSpeed + player.carData.maxSpeed / 100 * 4 * staticData.carPerks[staticData.selectedCarID][1];
        player.maxFuel = player.carData.maxFuel + player.carData.maxFuel / 100 * 5 * staticData.carPerks[staticData.selectedCarID][0];
        player.maxTorque = player.carData.maxTorque + player.carData.maxTorque / 100 * 6 * staticData.carPerks[staticData.selectedCarID][2];
        player.acceleration = player.carData.acceleration + player.carData.acceleration / 100 * 6 * staticData.carPerks[staticData.selectedCarID][2];
        player.maxDurability = player.carData.maxDurability + player.carData.maxDurability / 100 * 7 * staticData.carPerks[staticData.selectedCarID][3];
        player.currentDurability = player.maxDurability;
        player.currentFuel = player.maxFuel;
        ref var playerInventory = ref playerEntity.Get<Inventory>();
        playerInventory.inventory = new List<Product>();
        if (!staticData.trailerIsSelected)
        {
            playerInventory.maxMass = player.carData.carStorage + player.carData.carStorage / 100 * 5 * staticData.carPerks[staticData.selectedCarID][4];
        }
        else
        {
            playerInventory.maxMass = player.carData.carStorage + player.carData.trailerStorage + player.carData.carStorage / 100 * 5 * staticData.carPerks[staticData.selectedCarID][4];
        }

        playerEntity.Get<UpdateCargoRequest>();



        for (int i = 0; i < player.carData.playerCargo.Count; i++)
        {
            player.carData.playerCargoRB.Add(player.carData.playerCargo[i].gameObject.GetComponent<Rigidbody>());
            player.carData.playerCargoDefaultPos.Add(player.carData.playerCargo[i].transform.localPosition);
            player.carData.playerCargoDefaultRot.Add(player.carData.playerCargo[i].transform.localRotation);
        }
        if (staticData.trailerIsSelected)
        {
            player.carData.trailer.SetActive(true);
        }
        else
        {
            player.carData.trailer.SetActive(false);
        }
        player.activeWheelColliders = new List<WheelCollider>();
        foreach (var wc in player.carData.allWheelColliders)
        {
            if (wc.gameObject.activeInHierarchy)
            {
                player.activeWheelColliders.Add(wc);
            }
        }
        foreach (var wheel in player.carData.allWheelMeshes)
        {
            if (wheel.gameObject.activeInHierarchy)
            {
                player.carData.wheelDatas.Add(wheel.GetComponent<WheelData>());
            }
        }

        player.fuelConsumption = player.carData.drivingWheelColliders.Count * player.acceleration * player.activeWheelColliders[0].radius * player.activeWheelColliders[0].transform.localScale.y / 500;


        soundData.loopSounds.Add(player.carData.engineSound);
        soundData.SwitchLoopSounds(settings.sound);


        var virtualCam = sceneData.driveCam.GetComponent<CinemachineVirtualCamera>();
        virtualCam.Follow = player.playerGO.transform;
        virtualCam.LookAt = player.carData.cameraLookPoint;
    }

    void LabInit()
    {
        CreateResearchList();
        if (staticData.researchLvl < sceneData.researchList.Length)
        {
            buildingsData.labTradePoint.SetActive(true);
            var labEntity = _world.NewEntity();
            ref var labComp = ref labEntity.Get<ResearchLab>();
            ref var labBuyer = ref labEntity.Get<ProductBuyer>();
            labBuyer.buyingProductTypes = new List<ProductType>();
            labBuyer.buyerGO = buildingsData.labTradePoint;
            labBuyer.tradePointData = labBuyer.buyerGO.GetComponent<TradePointData>();
            labBuyer.repriceMultiplier = 1;
            ref var labInventory = ref labEntity.Get<Inventory>();
            labInventory.inventory = new List<Product>();
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


    void CreateResearchList()
    {
        sceneData.researchList = new Product[]{
        new Product(ProductType.Wheat, productData.wheat, 0),
        new Product(ProductType.Water, productData.water, 0),
        new Product(ProductType.Wheat, productData.wheat, 0),
        new Product(ProductType.Meat, productData.meat, 0),
        new Product(ProductType.Bread, productData.bread, 0),
        new Product(ProductType.Wheat, productData.wheat, 0),
        new Product(ProductType.Milk, productData.milk, 0),
        new Product(ProductType.Meat, productData.meat, 0),
        new Product(ProductType.Fish, productData.fish, 0),
        new Product(ProductType.Water, productData.water, 0),
        new Product(ProductType.Wheat, productData.wheat, 0),
        new Product(ProductType.Bread, productData.bread, 0),
        new Product(ProductType.Water, productData.water, 0),
        new Product(ProductType.Meat, productData.meat, 0),
        new Product(ProductType.Eggs, productData.eggs, 0),
        new Product(ProductType.Water, productData.water, 0),
        new Product(ProductType.Fruits, productData.fruits, 0),
        new Product(ProductType.Vegetables, productData.vegetables, 0),
        new Product(ProductType.Fruits, productData.fruits, 0),
        new Product(ProductType.Water, productData.water, 0),
        new Product(ProductType.Wheat, productData.wheat, 0),
        new Product(ProductType.Water, productData.water, 0),
        new Product(ProductType.Fruits, productData.fruits, 0),
        new Product(ProductType.Juice, productData.juice, 0),
        new Product(ProductType.Milk, productData.milk, 0),
        new Product(ProductType.Wheat, productData.wheat, 0),
        new Product(ProductType.Water, productData.water, 0),
        new Product(ProductType.Milk, productData.milk, 0),
        new Product(ProductType.Bread, productData.bread, 0),
        new Product(ProductType.Meat, productData.meat, 0),
        new Product(ProductType.Cheese, productData.cheese, 0),
        };



    }


    void LoadForTests()
    {
        settings.LoadPrefs();

        staticData.carPerks = new int[sceneData.cars.Count][];
        for (int i = 0; i < sceneData.cars.Count; i++)
        {
            staticData.carPerks[i] = new int[5];
        }

        int mapsCount = 1;
        staticData.mapPerks = new int[mapsCount][];
        for (int i = 0; i < mapsCount; i++)
        {
            staticData.mapPerks[i] = new int[5];
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
        }
    }

}
