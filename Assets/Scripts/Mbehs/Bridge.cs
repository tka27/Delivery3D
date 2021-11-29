using UnityEngine;

public class Bridge : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    [SerializeField] SceneData sceneData;

    void Start()
    {
        sceneData.allBridges.Add(this);
        sceneData.freeBridges.Add(this);
    }
}
