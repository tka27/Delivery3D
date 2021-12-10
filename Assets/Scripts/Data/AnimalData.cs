using UnityEngine;
using UnityEngine.AI;

public class AnimalData : MonoBehaviour
{
    public NavMeshAgent agent;
    public Rigidbody rb;
    [SerializeField] SceneData sceneData;
    public bool isAlive;
    string roadTag = "Road";



    void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > 1000)
        {
            isAlive = false;
            agent.enabled = false;
            rb.isKinematic = false;
            Debug.Log("Death");
        }
    }




    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == roadTag)
        {
            GameObject obstacle = sceneData.roadObstaclesPool[sceneData.roadObstaclesCurrentIndex];
            obstacle.SetActive(true);
            Transform colliderTf = collider.transform;
            float xRandom = Random.Range(-.5f, .5f) + colliderTf.position.x;
            float zRandom = Random.Range(-.5f, .5f) + colliderTf.position.z;
            obstacle.transform.position = new Vector3(xRandom, colliderTf.position.y, zRandom);

            sceneData.roadObstaclesCurrentIndex++;
            if (sceneData.roadObstaclesCurrentIndex == sceneData.roadObstaclesPool.Count)
            {
                sceneData.roadObstaclesCurrentIndex = 0;
            }
        }
    }
}
