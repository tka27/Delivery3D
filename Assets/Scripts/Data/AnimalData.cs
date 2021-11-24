using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalData : MonoBehaviour
{
    public NavMeshAgent agent;
    public Rigidbody rb;
    public bool isAlive;
    string roadTag = "Road";


    void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > 3000)
        {
            isAlive = false;
            agent.enabled = false;
            Debug.Log("Death");
        }
    }

    /*void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == roadTag)
        {
            Debug.Log(roadTag);
        }
    }*/
}
