using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarSwitcher : MonoBehaviour
{
    new Camera camera;
    Vector3 startPos;
    Vector3 tgtPos;
    [SerializeField] StaticData staticData;
    [SerializeField] MainMenuSceneData mainMenuSceneData;
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
        mainMenuSceneData.carsGameObjects[staticData.selectedCarID].gameObject.SetActive(false);
        if (xDifference > 0.2)
        {
            staticData.selectedCarID++;
            if (staticData.selectedCarID > mainMenuSceneData.carsGameObjects.Count - 1)
            {
                staticData.selectedCarID = 0;
            }
        }
        else if (xDifference < -0.2)
        {
            staticData.selectedCarID--;
            if (staticData.selectedCarID < 0)
            {
                staticData.selectedCarID = mainMenuSceneData.carsGameObjects.Count - 1;
            }
        }
        mainMenuSceneData.carsGameObjects[staticData.selectedCarID].gameObject.SetActive(true);
    }
}
