using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;


sealed class ViewCameraSystem : IEcsRunSystem, IEcsInitSystem
{
    SceneData sceneData;
    UIData uiData;
    float cameraHeight;
    Camera camera;
    Transform buildCameraPos;
    float minCameraHeight = 10;
    float maxCameraHeight = 100;
    Vector3 startPos = new Vector3();

    public void Init()
    {
        camera = Camera.main;
        buildCameraPos = sceneData.buildCam.transform;
    }

    void IEcsRunSystem.Run()
    {
        if (sceneData.gameMode == GameMode.View)
        {
            cameraHeight = buildCameraPos.position.y;
            if (Input.touchCount == 2 &&
            !UIData.IsMouseOverButton(uiData.buttons))
            {
                Touch firstTouch = Input.GetTouch(0);
                Touch secondTouch = Input.GetTouch(1);

                Vector2 firstTouchLastPos = firstTouch.position - firstTouch.deltaPosition;
                Vector2 secondTouchLastPos = secondTouch.position - secondTouch.deltaPosition;

                float lastDistance = (firstTouchLastPos - secondTouchLastPos).magnitude;
                float currentDistance = (firstTouch.position - secondTouch.position).magnitude;
                cameraHeight -= (currentDistance - lastDistance) * 0.01f;
            }
            else if (Input.GetMouseButton(0) &&
            !UIData.IsMouseOverButton(uiData.buttons))
            {
                Vector3 startPos = new Vector3(0.5f, 0, 0.5f);
                Vector3 tgtPos = new Vector3(camera.ScreenToViewportPoint(Input.mousePosition).x, 0, camera.ScreenToViewportPoint(Input.mousePosition).y);
                buildCameraPos.position += (tgtPos - startPos) * cameraHeight / 20;
            }
            cameraHeight = Mathf.Clamp(cameraHeight - Input.mouseScrollDelta.y, minCameraHeight, maxCameraHeight); //test wishlist
            buildCameraPos.position = new Vector3(buildCameraPos.position.x, cameraHeight, buildCameraPos.position.z);
        }
        else if (sceneData.gameMode == GameMode.Build)
        {
            cameraHeight = buildCameraPos.position.y;
            if (Input.GetMouseButtonDown(0))
            {
                startPos = new Vector3(camera.ScreenToViewportPoint(Input.mousePosition).x, 0, camera.ScreenToViewportPoint(Input.mousePosition).y);
            }
            else if (Input.GetMouseButton(0) &&
            !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 tgtPos = new Vector3(camera.ScreenToViewportPoint(Input.mousePosition).x, 0, camera.ScreenToViewportPoint(Input.mousePosition).y);
                buildCameraPos.position += (tgtPos - startPos) * cameraHeight / 50;
            }
        }
    }

}