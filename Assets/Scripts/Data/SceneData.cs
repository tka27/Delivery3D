using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class SceneData : MonoBehaviour
{
    public GameObject car;
    public GameMode gameMode;
    public Text gameModeText;
    public GameObject buildCam;
    public LineRenderer lineRenderer;
    public List<GameObject> waypointsPool;
    public List<Collider> wpColliders;
    public List<Transform> finalPoints;
    public GameObject wheatFarm;
    public List<GameObject> infoPanels;
}


