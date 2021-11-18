using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class SceneData : MonoBehaviour
{
    [SerializeField] ProductData productData;
    [SerializeField] GameObject notificationPanel;
    [SerializeField] Text notificationText;
    public AudioSource engineSound;
    public float groundHeight = -1.45f;
    public List<GameObject> cars;
    public GameMode gameMode;
    public GameObject buildCam;
    public GameObject driveCam;
    public LineRenderer lineRenderer;
    public List<GameObject> waypointsPool;
    public List<Collider> wpColliders;
    public List<Transform> finalPoints;
    public GameObject wheatFarmTradePoint;
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
    public List<GameObject> infoPanels;


    public void Notification(string notification)
    {
        notificationPanel.SetActive(true);
        notificationText.text = notification;
    }
}


