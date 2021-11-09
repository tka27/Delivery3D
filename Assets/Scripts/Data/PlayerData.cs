using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public List<Transform> allWheelMeshes;
    public List<WheelCollider> allWheelColliders;
    public List<WheelCollider> steeringWheelColliders;
    public List<WheelCollider> drivingWheelColliders;
    public Transform wheelPos;
    public Transform centerOfMass;
    public List<WheelData> wheelDatas;
    public Collider carCollider;
    public List<GameObject> playerCargo;
    public List<Rigidbody> playerCargoRB;
    public List<Vector3> playerCargoDefaultPos;
    public List<Quaternion> playerCargoDefaultRot;
    public GameObject inventoryCanvas;
    public GameObject clearInventoryBtn;
}
