using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class SceneData : MonoBehaviour
{
    [SerializeField] ProductData productData;
    [SerializeField] GameObject notificationPanel;
    [SerializeField] Text notificationText;
    public static Transform lastVisit;
    public List<GameObject> cars;
    public List<TrailRenderer> trails;
    public GameMode gameMode;
    public GameObject buildCam;
    public GameObject driveCam;

    public List<Transform> animalSpawnPoints;
    public List<GameObject> animalsPool;
    public List<GameObject> roadObstaclesPool;
    public int roadObstaclesCurrentIndex;

    public List<Transform> allCoinsPositions;
    public List<Transform> emptyCoinsPositions;
    public List<GameObject> coinsPool;

    [HideInInspector] public List<GameObject> tradePointCanvases;
    public GameObject wheatTradePoint;
    public GameObject waterTradePoint;
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
    public GameObject fishTradePoint;
    public GameObject canFishTradePoint;
    public GameObject fruitTradePoint;
    public GameObject vegetableTradePoint;
    public GameObject juiceTradePoint;
    public GameObject iceTradePoint;



    public List<Product> researchList = new List<Product>();


    public void Notification(string notification)
    {
        notificationPanel.SetActive(true);
        notificationText.text = notification;
    }
}


