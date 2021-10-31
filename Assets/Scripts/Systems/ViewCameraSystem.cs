using Leopotam.Ecs;
using UnityEngine;


sealed class ViewCameraSystem : IEcsRunSystem, IEcsInitSystem
{
    SceneData sceneData;
    Vector3 startPos;
    float cameraHeight;
    Camera camera;

    public void Init()
    {
        startPos = sceneData.buildCam.transform.position;
        camera = Camera.main;
    }

    void IEcsRunSystem.Run()
    {
        if (sceneData.gameMode == GameMode.View)
        {
            RaycastHit hit;
            Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(mouseRay, out hit, 1000);

            if (Input.GetMouseButtonDown(0) &&
            !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())//check ui button)
            {
                startPos = hit.point;
            }
            else if (Input.GetMouseButton(0) &&
            !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 tgtPos = hit.point;
                sceneData.buildCam.transform.position += startPos - tgtPos;
            }
        }

    }
}