using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.AI;
public class SceneData : MonoBehaviour
{
    [SerializeField] GameObject notificationPanel;
    [SerializeField] Text notificationText;
    public List<GameObject> cars;
    public GameMode gameMode;
    public GameObject buildCam;
    public GameObject driveCam;

    public List<Transform> animalSpawnPoints;
    public NavMeshSurface navMeshSurface;
    public List<GameObject> animalsPool;
    public List<GameObject> roadObstaclesPool;
    public int roadObstaclesCurrentIndex;

    public List<Transform> allCoinsPositions;
    [HideInInspector] public List<Transform> emptyCoinsPositions;
    public List<GameObject> coinsPool;





    public List<Product> researchList = new List<Product>();


    public void Notification(string notification)
    {
        notificationPanel.SetActive(true);
        notificationText.text = notification;
    }
}


