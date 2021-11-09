using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PathComp
{
    public List<GameObject> wayPoints;
    public List<Collider> wpColliders;
    public int currentWaypointIndex;
    public LineRenderer lineRenderer;
    public List<GameObject> waypointsPool;
    public int currentPoolIndex;
}
public struct PlayerComp
{
    public Rigidbody playerRB;
    public GameObject playerGO;
    public CarData carData;
    public float maxSteerAngle;
    public float maxTorque;
    public float currentTorque;
    public float acceleration;
    public float maxDurability;
    public float currentDurability;
    public float maxFuel;
    public float currentFuel;
    public float fuelConsumption;
    public float defaultRBMass;
}
public struct MovableComp
{
}
public struct DestroyRoadRequest
{
}
public struct ImmobilizeRequest
{
}
public struct ProductBuyer
{
    public GameObject buyerGO;
    public TradePointData tradePointData;
    public Product product;
    public float currentPrice;
    public float repriceMultiplier;
}
public struct ProductSeller
{
    public GameObject sellerGO;
    public TradePointData tradePointData;
    public Product product;
    public float produceSpeed;
    public float currentPrice;
    public float repriceMultiplier;
    public float timer;
}
public struct StorageComp
{
    public float maxMass;
    public float currentMass;
}
public struct CargoComp
{
    public List<Product> inventory;
}
public struct AutoService
{
}
public struct SellDataUpdateRequest
{
}
public struct BuyDataUpdateRequest
{
}
public struct Quest
{
    public float currentQuestTime;
    public float maxQuestTime;
    public float timer;
}
public struct UpdateCargoRequest
{
}
public struct CratesDisplayRequest
{
}
public struct ResearchLab
{
    public float progress;
}
