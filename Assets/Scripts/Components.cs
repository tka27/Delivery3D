using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PathComp
{
    public List<GameObject> wayPoints;
}
public struct PlayerComp
{
    public GameObject playerGO;
    public PlayerData playerData;
    public float maxSteerAngle;
    public float maxTorque;
    public float currentSpeed;
    public float acceleration;
}
public struct MovableComp
{
    public float moveSpeed;
}
public struct LineComp
{
    public LineRenderer lineRenderer;
}
