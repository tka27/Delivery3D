using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CarSwitcher : MonoBehaviour
{
    new Camera camera;
    Vector3 startPos;
    Vector3 tgtPos;
    [SerializeField] StaticData staticData;
    [SerializeField] MainMenuSceneData mainMenuSceneData;
    [SerializeField] UnityEvent carInfoUpdateEvent;
    [SerializeField] GameObject upgradeCanvas;
    bool tryToSwitch;

    void Start()
    {
        camera = Camera.main;
    }
    void Update()
    {
        if (upgradeCanvas.activeSelf) return;

        float xDifference = 0;
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            startPos = camera.ScreenToViewportPoint(Input.mousePosition);
            tryToSwitch = true;
        }
        else if (Input.GetMouseButtonUp(0) && tryToSwitch)
        {
            tgtPos = camera.ScreenToViewportPoint(Input.mousePosition);
            xDifference = startPos.x - tgtPos.x;
        }


        if (xDifference > 0.2 && tryToSwitch)
        {
            tryToSwitch = false;
            mainMenuSceneData.cars[staticData.selectedCarID].gameObject.SetActive(false);
            staticData.selectedCarID++;
            if (staticData.selectedCarID > mainMenuSceneData.cars.Count - 1)
            {
                staticData.selectedCarID = 0;
            }
            carInfoUpdateEvent.Invoke();
        }
        else if (xDifference < -0.2 && tryToSwitch)
        {
            tryToSwitch = false;
            mainMenuSceneData.cars[staticData.selectedCarID].gameObject.SetActive(false);
            staticData.selectedCarID--;
            if (staticData.selectedCarID < 0)
            {
                staticData.selectedCarID = mainMenuSceneData.cars.Count - 1;
            }
            carInfoUpdateEvent.Invoke();
        }
    }
}
