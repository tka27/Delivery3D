using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRoadSystem : MonoBehaviour
{
    public GameObject waypointPrefab;
    LayerMask layer;
    List<GameObject> waypoints;
    TrailRenderer trail;
    void Start()
    {
        waypoints = new List<GameObject>();
        layer = LayerMask.GetMask("Ground");
        trail = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        Vector3 waypointPos;
        Vector3 cameraPos = Camera.main.transform.position;
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (waypoints.Count == 0)
        {
            trail.enabled = false;
        }
        else
        {
            trail.enabled = true;
        }
        if (Input.GetMouseButton(0) && Physics.Raycast(mouseRay, out hit, 1000, layer))
        {
            waypointPos = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
            waypoints.Add(Instantiate(waypointPrefab, waypointPos, Quaternion.identity));
            trail.transform.position = waypointPos;
        }
    }
}
