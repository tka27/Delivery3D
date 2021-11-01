using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public List<WheelCollider> wheelColliders;
    public List<Transform> wheelMeshes;
    public Transform wheelPos;
    public Transform centerOfMass;
    public List<WheelData> wheelDatas;
}
