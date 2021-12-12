using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.AI;
public class SceneData : MonoBehaviour
{
    [SerializeField] GameObject notificationPanel;
    [SerializeField] Text notificationText;
    public List<GameObject> cars;
    [HideInInspector] public GameMode gameMode;
    public GameObject buildCam;
    public GameObject driveCam;

    public List<Transform> animalSpawnPoints;
    public NavMeshSurface navMeshSurface;
    public List<GameObject> animalsPool;
    public List<GameObject> roadObstaclesPool;
    [HideInInspector] public int roadObstaclesCurrentIndex;

    public List<Transform> allCoinsPositions;
    [HideInInspector] public List<Transform> emptyCoinsPositions;
    public List<GameObject> coinsPool;





    [HideInInspector] public Product[] researchList;
    public AnimationCurve researchCurve;
    [HideInInspector] public float researchSpeed = 1;


    public void Notification(string notification)
    {
        notificationPanel.SetActive(true);
        notificationText.text = notification;
    }
}


