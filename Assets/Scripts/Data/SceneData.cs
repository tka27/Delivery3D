using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class SceneData : MonoBehaviour
{
    [SerializeField] ProductData productData;
    [SerializeField] GameObject notificationPanel;
    [SerializeField] Text notificationText;
    public List<GameObject> cars;
    public GameMode gameMode;
    public GameObject buildCam;
    public GameObject driveCam;
    public LineRenderer lineRenderer;
    public List<GameObject> waypointsPool;
    public List<Transform> finalPoints;

    public List<Transform> animalSpawnPoints;
    public List<GameObject> animalsPool;
    public List<GameObject> roadObstaclesPool;
    public int roadObstaclesCurrentIndex;

    public List<Transform> allCoinsPositions;
    public List<Transform> emptyCoinsPositions;
    public List<GameObject> coinsPool;

    [HideInInspector] public List<GameObject> tradePointCanvases;
    public GameObject wheatFarmTradePoint;
    public GameObject waterStationTradePoint;
    public GameObject bakeryTradePoint;
    public GameObject chickenTradePoint;
    public GameObject meatFactoryTradePoint;
    public GameObject milkFactoryTradePoint;
    public GameObject pizzaTradePoint;
    public GameObject cheeseTradePoint;
    public GameObject gasStationTradePoint;
    public GameObject autoServiceTradePoint;
    public GameObject shopTradePoint;
    public GameObject labTradePoint;



    public List<Product> researchList = new List<Product>();


    public void Notification(string notification)
    {
        notificationPanel.SetActive(true);
        notificationText.text = notification;
    }
}


