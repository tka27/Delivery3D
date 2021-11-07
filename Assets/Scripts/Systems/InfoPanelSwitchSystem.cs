using Leopotam.Ecs;
using UnityEngine;


sealed class InfoPanelSwitchSystem : IEcsRunSystem
{
    SceneData sceneData;
    void IEcsRunSystem.Run()
    {
        if (sceneData.gameMode != GameMode.Drive && !sceneData.infoPanels[0].activeSelf)
        {
            foreach (var panel in sceneData.infoPanels)
            {
                panel.SetActive(true);
            }
        }
        else if (sceneData.gameMode == GameMode.Drive && sceneData.infoPanels[0].activeSelf)
        {
            foreach (var panel in sceneData.infoPanels)
            {
                panel.SetActive(false);
            }
        }
    }
}