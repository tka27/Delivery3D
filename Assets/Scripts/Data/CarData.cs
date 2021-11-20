using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarData : MonoBehaviour
{
    public float price;
    public List<Transform> allWheelMeshes;
    public List<WheelCollider> allWheelColliders;
    public List<WheelCollider> steeringWheelColliders;
    public List<WheelCollider> drivingWheelColliders;
    public List<WheelCollider> brakingWheelColliders;
    public Transform wheelPos;
    public Transform centerOfMass;
    public Transform cameraLookPoint;
    [HideInInspector] public List<WheelData> wheelDatas;
    public Collider carCollider;
    public GameObject trailer;
    public List<GameObject> playerCargo;
    public List<Rigidbody> playerCargoRB;
    public List<Vector3> playerCargoDefaultPos;
    public List<Quaternion> playerCargoDefaultRot;
    public GameObject inventoryCanvas;
    public GameObject clearInventoryBtn;
    public AudioSource engineSound;
    public float enginePitchDefault;

    public float maxSteerAngle;
    public float maxTorque;
    public float maxDurability;
    public float acceleration;
    public float maxFuel;
    public float defaultMass;
    public float carStorage;
    public float trailerStorage;

}
