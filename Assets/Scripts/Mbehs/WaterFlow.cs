using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    [SerializeField] Material material;
    float offset;
    string playerTag = "Player";
    string animalTag = "Animal";
    void FixedUpdate()
    {
        offset += 0.001f;
        material.mainTextureOffset = new Vector2(0, offset);
    }
    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == playerTag)
        {
            Rigidbody rb = collider.attachedRigidbody;
            rb?.AddForce(Vector3.up * rb.mass * 3);
            rb?.AddForce(-rb.velocity * rb.mass * 10);
        }
        else if (collider.tag == animalTag)
        {
            Rigidbody rb = collider.attachedRigidbody;
            rb?.AddForce(Vector3.up * rb.mass * 55);
            rb?.AddForce(-rb.velocity * rb.mass * 40);
        }
    }
}
