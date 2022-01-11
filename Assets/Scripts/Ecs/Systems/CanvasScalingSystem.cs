using Leopotam.Ecs;
using UnityEngine;


sealed class CanvasScalingSystem : IEcsRunSystem
{
    SceneData sceneData;
    BuildingsData buildingsData;
    void IEcsRunSystem.Run()
    {
        if (sceneData.gameMode == GameMode.Drive) return;

        float multiplier = Mathf.Clamp(sceneData.buildCam.transform.position.y / 35, 1, 6);
        Vector3 scale = new Vector3(multiplier, multiplier, 0);

        foreach (var canvas in buildingsData.tradePointCanvases)
        {
            canvas.transform.localScale = scale;
        }
    }
}



