using Leopotam.Ecs;
using UnityEngine;


sealed class InfoPanelSwitchSystem : IEcsRunSystem
{
    SceneData sceneData;
    void IEcsRunSystem.Run()
    {
        if (sceneData.gameMode != GameMode.Drive && sceneData.buildCam.transform.position.y > 10 && !sceneData.tradePointCanvases[0].activeSelf)
        {
            foreach (var panel in sceneData.tradePointCanvases)
            {
                panel.SetActive(true);
            }
        }
        else if (sceneData.gameMode == GameMode.Drive && sceneData.tradePointCanvases[0].activeSelf || sceneData.buildCam.transform.position.y <= 10 && sceneData.tradePointCanvases[0].activeSelf)
        {
            foreach (var panel in sceneData.tradePointCanvases)
            {
                panel.SetActive(false);
            }
        }
    }
}