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
    public PlayerData playerData;
    public float maxSteerAngle;
    public float maxTorque;
    public float currentTorque;
    public float acceleration;
    public float maxDurability;
    public float currentDurability;
    public float maxFuel;
    public float currentFuel;
    public float fuelConsumption;
    public int money;
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
    public ProductType buyingProduct;
    public int buyingProductCount;
    public ProductType sellingProduct;
    public int sellingProductCount;
    public float produceSpeed;
    public int buyPrice;
    public int sellPrice;
}
public struct ProductSeller
{
    public GameObject sellerGO;
    public TradePointData tradePointData;
    public ProductType sellingProduct;
    public int sellingProductCount;
    public float produceSpeed;
    public int sellPrice;
}
public struct StorageComp
{
    public int maxMass;
    public int currentMass;
}
public struct CargoComp
{
    public List<Cargo> inventory;
}
/*public struct BuyRequest
{
}*/

