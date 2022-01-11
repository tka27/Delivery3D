using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CarData : MonoBehaviour
{
    public float price;
    public GameObject trailer;
    public Rigidbody trailerRB;
    [SerializeField] Rigidbody selfRB;
    public List<ProductType> carProductTypes;
    public List<ProductType> trailerProductTypes;
    public List<Transform> allWheelMeshes;
    public List<WheelCollider> allWheelColliders;
    public List<WheelCollider> steeringWheelColliders;
    public List<WheelCollider> drivingWheelColliders;
    public List<WheelCollider> brakingWheelColliders;
    public Transform wheelPos;
    public Transform centerOfMass;
    public Transform cameraLookPoint;
    [HideInInspector] public List<WheelData> wheelDatas;
    public List<GameObject> playerCargo;
    public List<Rigidbody> playerCargoRB;
    [HideInInspector] public List<Vector3> playerCargoDefaultPos;
    [HideInInspector] public List<Quaternion> playerCargoDefaultRot;
    public AudioSource engineSound;
    public float enginePitchDefault;

    public float maxSteerAngle;
    public float maxSpeed;
    public float maxTorque;
    public float maxDurability;
    public float acceleration;
    public float maxFuel;
    public float defaultMass;
    public float carStorage;
    public float trailerStorage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Building")
        {
            StartCoroutine(StopCar());
        }
    }

    IEnumerator StopCar()
    {
        selfRB.drag = 1;

        yield return new WaitForSeconds(3);
        selfRB.drag = 0;
    }

}
