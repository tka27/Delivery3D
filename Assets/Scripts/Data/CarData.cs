using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarData : MonoBehaviour
{
    public int carID;
    public float price;
    public bool unlocked;
    public bool bought;
    public int fuelLvl;
    public int suspensionLvl;
    public List<Transform> allWheelMeshes;
    public List<WheelCollider> allWheelColliders;
    public List<WheelCollider> steeringWheelColliders;
    public List<WheelCollider> drivingWheelColliders;
    public Transform wheelPos;
    public Transform centerOfMass;
    public Transform cameraLookPoint;
    public List<WheelData> wheelDatas;
    public Collider carCollider;
    public List<GameObject> playerCargo;
    public List<Rigidbody> playerCargoRB;
    public List<Vector3> playerCargoDefaultPos;
    public List<Quaternion> playerCargoDefaultRot;
    public GameObject inventoryCanvas;
    public GameObject clearInventoryBtn;

    public float maxSteerAngle;
    public float maxTorque;
    public float maxDurability;
    public float acceleration;
    public float maxFuel;
    public float fuelConsumption;
    public float maxStorageMass;
    
}
