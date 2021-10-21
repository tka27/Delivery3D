using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputComp
{
    public Vector3 mouseWorldPos;
    public bool spaceDown;
    public bool mouse0Down;
}
public struct PathComp
{
    public List<GameObject> wayPoints;
}
public struct PlayerComp
{
    public GameObject playerGO;
    public Rigidbody2D wheelsRB;
}
public struct MovableComp
{
    public float moveSpeed;
}
public struct LineComp{
    public LineRenderer lineRenderer;
}
