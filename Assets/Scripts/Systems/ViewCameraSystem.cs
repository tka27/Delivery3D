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
    float maxCameraHeight = 388;
    Vector3 startPos = new Vector3();
    bool moveMode;

    public void Init()
    {
        camera = Camera.main;
        buildCameraPos = sceneData.buildCam.transform;
    }

    void IEcsRunSystem.Run()
    {
        if (sceneData.gameMode == GameMode.Drive) return;
        cameraHeight = buildCameraPos.position.y;
        if (buildCameraPos.position != camera.transform.position)
        {
            BorderCheck();
            return;
        }
        if (Input.touchCount == 2 &&
        !UIData.IsMouseOverButton(uiData.buttons))
        {
            moveMode = false;
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            Vector2 firstTouchLastPos = firstTouch.position - firstTouch.deltaPosition;
            Vector2 secondTouchLastPos = secondTouch.position - secondTouch.deltaPosition;

            float lastDistance = (firstTouchLastPos - secondTouchLastPos).magnitude;
            float currentDistance = (firstTouch.position - secondTouch.position).magnitude;
            cameraHeight -= (currentDistance - lastDistance) * cameraHeight / 500;
        }
        cameraHeight = Mathf.Clamp(cameraHeight - (Input.mouseScrollDelta.y * cameraHeight / 5), minCameraHeight, maxCameraHeight);
        MoveCamera();
    }
    Vector3 GetWorldPosition(float y)
    {
        Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0, y, 0));
        float distance;
        plane.Raycast(mouseRay, out distance);
        return mouseRay.GetPoint(distance);
    }
    void BorderCheck()
    {
        float xPos = Mathf.Clamp(buildCameraPos.position.x, -maxCameraHeight + cameraHeight, maxCameraHeight - cameraHeight);
        float zPos = Mathf.Clamp(buildCameraPos.position.z, -maxCameraHeight + cameraHeight * .55f, maxCameraHeight - cameraHeight * .55f);
        buildCameraPos.position = new Vector3(xPos, cameraHeight, zPos);
    }
    void MoveCamera()
    {
        if (sceneData.gameMode == GameMode.View)
        {
            if (Input.GetMouseButtonDown(0) && !UIData.IsMouseOverButton(uiData.buttons))
            {
                startPos = GetWorldPosition(0);
                moveMode = true;
            }
            else if (Input.GetMouseButton(0) && moveMode && !UIData.IsMouseOverButton(uiData.buttons))
            {
                Vector3 tgtPos = startPos - GetWorldPosition(0);
                buildCameraPos.position += tgtPos;
            }

            BorderCheck();
        }
        else if (sceneData.gameMode == GameMode.Build)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = new Vector3(camera.ScreenToViewportPoint(Input.mousePosition).x, 0, camera.ScreenToViewportPoint(Input.mousePosition).y);
            }
            else if (Input.GetMouseButton(0) &&
            !UIData.IsMouseOverButton(uiData.buttons))
            {
                Vector3 tgtPos = new Vector3(camera.ScreenToViewportPoint(Input.mousePosition).x, 0, camera.ScreenToViewportPoint(Input.mousePosition).y);
                buildCameraPos.position += (tgtPos - startPos) * cameraHeight / 50;
            }

            BorderCheck();
        }
    }
}