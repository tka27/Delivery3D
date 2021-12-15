using Leopotam.Ecs;
using UnityEngine;


sealed class TutorialSystem : IEcsRunSystem, IEcsInitSystem
{
    StaticData staticData;
    SceneData sceneData;
    PathData pathData;
    BuildingsData buildingsData;
    UIData uiData;
    GameSettings settings;
    EcsFilter<Inventory, Player> playerFilter;
    TutorialData tutorialData;
    public void Init()
    {
        tutorialData = sceneData.tutorial.GetComponent<TutorialData>();
        //tutorialData = GameObject.Instantiate(staticData.tutorialPrefab).GetComponent<TutorialData>();
        settings.tutorialLvl = 0;

        tutorialData.yellowText.gameObject.SetActive(true);
        tutorialData.yellowText.text = "This is the game mode button.\nIn view mode you can move/zoom camera and see some info.\nTap on button to change mode";
        tutorialData.blackPanel.gameObject.SetActive(true);
        tutorialData.blackText.text = "In build mode you can build road.\nTap inside yellow sphere for building";
    }

    void IEcsRunSystem.Run()
    {
        switch (settings.tutorialLvl)
        {
            case 0:
                if (sceneData.gameMode == GameMode.Build)
                {
                    settings.tutorialLvl++;
                    tutorialData.blackText.gameObject.SetActive(true);
                    SwitchBlackPanel(false);
                }
                break;

            case 1:
                if (pathData.wayPoints.Count != 0)
                {
                    settings.tutorialLvl++;
                    SwitchBlackPanel(true);
                    tutorialData.blackPanel.SetTransform(tutorialData.clearBtnPos);

                    tutorialData.yellowText.text = "Destroy road button will remove the road.\nTap on the button to continue";
                }
                break;

            case 2:
                if (pathData.wayPoints.Count == 0)
                {
                    settings.tutorialLvl++;
                    SwitchBlackPanel(false);
                    tutorialData.blackText.text = "Build road to wheat farm";
                }
                break;

            case 3:
                MoveCamera(tutorialData.wheatFarmPos.position);
                break;

            case 4:
                if (uiData.isPathConfirmed)
                {
                    settings.tutorialLvl++;
                    tutorialData.blackText.text = "In drive mode tap on the screen to accelerate.\nAcceleration is consuming fuel ";
                }
                break;

            case 5:
                if (playerFilter.Get2(0).playerRB.velocity.magnitude > 2)
                {
                    settings.tutorialLvl++;
                    tutorialData.blackText.text = "Drive to the farm";
                }
                break;

            case 6:
                if (pathData.wayPoints.Count == 0)
                {
                    settings.tutorialLvl++;
                    tutorialData.farmPanel.SetActive(true);
                    SwitchBlackPanel(true);
                    tutorialData.blackPanel.SetTransform(tutorialData.buyBtnPos);
                }
                break;

            case 7:
                if (playerFilter.Get1(0).HasItem(ProductType.Wheat, 200))
                {
                    settings.tutorialLvl++;
                    SwitchBlackPanel(false);
                    tutorialData.farmPanel.SetActive(false);
                    tutorialData.blackText.text = "Zoom in the camera to open your inventory";
                }
                break;

            case 8:
                if (uiData.inventoryCanvas.activeSelf)
                {
                    tutorialData.nextBtn.SetActive(true);
                    tutorialData.blackText.text = "Clear button will drop all items from your inventory";
                }
                break;

            case 9:

                settings.tutorialLvl++;
                tutorialData.blackText.text = "Deliver 200kg of wheat to the quality control lab";
                MoveCamera(tutorialData.labPos.position);

                break;

            case 10:
                if (sceneData.gameMode == GameMode.Drive)
                {
                    settings.tutorialLvl++;
                    tutorialData.blackText.text = "When your wheels are off road you take damage based on speed";
                    MoveCamera(tutorialData.labPos.position);
                }

                break;

            case 11:

                tutorialData.blackText.text = "When your wheels are off road you take damage based on speed";
                MoveCamera(tutorialData.labPos.position);

                break;


            default: return;
        }
    }
    void SwitchBlackPanel(bool value)
    {
        tutorialData.blackPanel.gameObject.SetActive(value);
        if (value)
        {
            tutorialData.blackText.text = "";
        }
        else
        {
            tutorialData.yellowText.text = "";
        }
    }
    void MoveCamera(in Vector3 tgt)
    {
        sceneData.buildCam.position = Vector3.Lerp(sceneData.buildCam.position, tgt, .03f);

        if ((sceneData.buildCam.position - tgt).magnitude < .5f)
        {
            settings.tutorialLvl++;
        }
    }

}



