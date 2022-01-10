using UnityEngine;
using System.Collections.Generic;

public class BuildingsData : MonoBehaviour
{
    [HideInInspector] public List<GameObject> tradePointCanvases;
    public BuildingsInitSystem buildingsInitSystem;
    public static TradePointData lastVisit;
    public GameObject wheatTradePoint;
    public GameObject waterTradePoint;
    public GameObject bakeryTradePoint;
    public GameObject chickenTradePoint;
    public GameObject chickenGO;
    public GameObject meatFactoryTradePoint;
    public GameObject milkFactoryTradePoint;
    public GameObject cowFarmGO;
    public GameObject pizzaTradePoint;
    public GameObject cheeseTradePoint;
    public GameObject cheeseGO;
    public GameObject gasStationTradePoint;
    public GameObject autoServiceTradePoint;
    public GameObject fishTradePoint;
    public GameObject fishGO;
    public GameObject canFishTradePoint;
    public GameObject canFishGO;
    public GameObject fruitTradePoint;
    public GameObject fruitGO;
    public GameObject vegetableTradePoint;
    public GameObject vegetableGO;
    public GameObject juiceTradePoint;
    public GameObject juiceGO;
    public GameObject iceTradePoint;
    public GameObject iceGO;


    public GameObject shop1TradePoint;
    public GameObject shop2TradePoint;
    public GameObject labTradePoint;
}
