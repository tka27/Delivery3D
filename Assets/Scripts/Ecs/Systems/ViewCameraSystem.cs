using Leopotam.Ecs;
using UnityEngine;


sealed class ViewCameraSystem : IEcsRunSystem, IEcsInitSystem
{
    SceneData sceneData;
    UIData uiData;
    float cameraHeight;
    Camera camera;
    const float MIN_CAM_HEIGHT = 10;
    const float MAX_Y_BORDER = 388;
    float MAX_CAM_HEIGHT = 200;
    Vector3 startPos = new Vector3();
    bool moveMode;

    public void Init()
    {
        camera = Camera.main;
    }

    void IEcsRunSystem.Run()
    {
        if (sceneData.gameMode == GameMode.Drive) return;
        cameraHeight = sceneData.buildCam.position.y;
        if (sceneData.buildCam.position != camera.transform.position)
        {
            BorderCheck();
            return;
        }
        if (Input.touchCount == 2 &&
        !UIData.IsMouseOverUI() && sceneData.gameMode == GameMode.View)
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
        cameraHeight = Mathf.Clamp(cameraHeight - (Input.mouseScrollDelta.y * cameraHeight / 5), MIN_CAM_HEIGHT, MAX_CAM_HEIGHT);
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
        float xPos = Mathf.Clamp(sceneData.buildCam.position.x, -MAX_Y_BORDER + cameraHeight, MAX_Y_BORDER - cameraHeight);
        float zPos = Mathf.Clamp(sceneData.buildCam.position.z, -MAX_Y_BORDER + cameraHeight * .55f, MAX_Y_BORDER - cameraHeight * .55f);
        sceneData.buildCam.position = new Vector3(xPos, cameraHeight, zPos);
    }
    void MoveCamera()
    {
        if (sceneData.gameMode == GameMode.View)
        {
            if (Input.GetMouseButtonDown(0) && !UIData.IsMouseOverUI())//IsMouseOverButton(uiData.buttons))
            {
                startPos = GetWorldPosition(0);
                moveMode = true;
            }
            else if (Input.GetMouseButton(0) && moveMode && !UIData.IsMouseOverUI())
            {
                Vector3 tgtPos = startPos - GetWorldPosition(0);
                sceneData.buildCam.position += tgtPos;
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
            !UIData.IsMouseOverUI())
            {
                Vector3 tgtPos = new Vector3(camera.ScreenToViewportPoint(Input.mousePosition).x, 0, camera.ScreenToViewportPoint(Input.mousePosition).y);
                sceneData.buildCam.position += (tgtPos - startPos) * cameraHeight / 40;
            }
            BorderCheck();
        }
    }
}