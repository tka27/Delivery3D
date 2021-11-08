using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class SceneData : MonoBehaviour
{
    public float groundHeight = -1.45f;
    public GameObject car;
    public GameMode gameMode;
    public GameObject buildCam;
    public LineRenderer lineRenderer;
    public List<GameObject> waypointsPool;
    public List<Collider> wpColliders;
    public List<Transform> finalPoints;
    public GameObject wheatFarmTradePoint;
    public GameObject bakeryTradePoint;
    public GameObject gasStationTradePoint;
    public GameObject autoServiceTradePoint;
    public GameObject shopTradePoint;


    public List<GameObject> infoPanels;
}


