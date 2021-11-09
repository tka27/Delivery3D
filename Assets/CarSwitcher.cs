using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarSwitcher : MonoBehaviour
{
    new Camera camera;
    Vector3 startPos;
    Vector3 tgtPos;
    void Start()
    {
        camera = Camera.main;
    }
    void Update()
    {
        float xDifference = 0;
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            startPos = camera.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            tgtPos = camera.ScreenToViewportPoint(Input.mousePosition);
            xDifference = startPos.x - tgtPos.x;
        }
        if (xDifference > 0.2)
        {
            Debug.Log("+1");
        }
        else if (xDifference < -0.2)
        {
            Debug.Log("-1");
        }
    }
}
