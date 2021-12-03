using Leopotam.Ecs;



sealed class CameraSwitchSystem : IEcsRunSystem
{
    SceneData sceneData;

    void IEcsRunSystem.Run()
    {
        if (sceneData.gameMode == GameMode.View && !sceneData.buildCam.activeSelf || sceneData.gameMode == GameMode.Build && !sceneData.buildCam.activeSelf) //camera view build
        {
            sceneData.buildCam.SetActive(true);
        }
        else if (sceneData.gameMode == GameMode.Drive && sceneData.buildCam.activeSelf) //camera drive
        {
            sceneData.buildCam.SetActive(false);
        }
    }
}
