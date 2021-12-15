using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalData : MonoBehaviour
{
    public NavMeshAgent agent;
    public List<Rigidbody> allRB;
    [SerializeField] SceneData sceneData;
    [SerializeField] Animator animator;
    [SerializeField] Collider bodyCollider;
    [SerializeField] List<Collider> ragdollColliders;
    bool obstacleCD;
    public bool isAlive;
    const string ROAD_TAG = "Road";
    const string PLAYER_TAG = "Player";
    const float DEADLY_SPEED = 10;
    const int MIN_RADIUS = 20;
    const int SEARCH_RADIUS = 150;


    void Start()
    {
        sceneData.animalsPool.Add(this);
        Revive();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == ROAD_TAG && !obstacleCD)
        {
            StartCoroutine(OnRoadCrossing(collider));
        }
        else if (collider.tag == PLAYER_TAG && collider.attachedRigidbody.velocity.magnitude > DEADLY_SPEED)
        {
            Debug.Log(collider.attachedRigidbody.velocity.magnitude);
            Kill();
        }

    }

    public void Revive()
    {
        /*int randomIndex = Random.Range(0, sceneData.animalPoints.Count);
        transform.position = sceneData.animalPoints[randomIndex].position;*/

        //animator.gameObject.SetActive(true);
        SwitchComponents(true);
    }

    void Kill()
    {
        SwitchComponents(false);
        Debug.Log("Death");
    }

    void SwitchComponents(bool value)
    {
        isAlive = value;
        bodyCollider.enabled = value;
        agent.enabled = value;
        animator.enabled = value;
        foreach (var rb in allRB)
        {
            rb.isKinematic = value;
            rb.useGravity = !value;
        }
        foreach (var collider in ragdollColliders)
        {
            collider.enabled = !value;
        }

    }
    IEnumerator OnRoadCrossing(Collider collider)
    {
        obstacleCD = true;
        yield return new WaitForSeconds(1);
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
        yield return new WaitForSeconds(2);
        obstacleCD = false;
    }


    public void SetPath()
    {
        List<Transform> availablePoints = new List<Transform>();
        foreach (var point in sceneData.animalPoints)
        {
            if ((point.position - transform.position).magnitude < SEARCH_RADIUS && (point.position - transform.position).magnitude > MIN_RADIUS)
            {
                availablePoints.Add(point);
            }
        }
        if (availablePoints.Count == 0) return;

        int randomIndex = Random.Range(0, availablePoints.Count);
        if ((availablePoints[randomIndex].localPosition - transform.localPosition).magnitude < MIN_RADIUS)
        {
            SetPath();
            return;
        }
        agent.SetDestination(availablePoints[randomIndex].position);
    }

}
