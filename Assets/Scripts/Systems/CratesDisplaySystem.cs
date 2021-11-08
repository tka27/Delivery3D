using Leopotam.Ecs;


sealed class CratesDisplaySystem : IEcsRunSystem
{

    EcsFilter<PlayerComp, StorageComp, CratesDisplayRequest> playerFilter;

    void IEcsRunSystem.Run()
    {
        foreach (var fPlayer in playerFilter)
        {
            ref var player = ref playerFilter.Get1(fPlayer);
            ref var playerStorage = ref playerFilter.Get2(fPlayer);
            float filledPart = playerStorage.currentMass / playerStorage.maxMass;

            for (int i = 0; i < player.playerData.playerCargo.Count * filledPart; i++)
            {
                player.playerData.playerCargo[i].SetActive(true);
                player.playerData.playerCargoRB[i].isKinematic = true;
                player.playerData.playerCargo[i].transform.localPosition = player.playerData.playerCargoDefaultPos[i];
                player.playerData.playerCargo[i].transform.localRotation = player.playerData.playerCargoDefaultRot[i];
            }
            playerFilter.GetEntity(fPlayer).Del<CratesDisplayRequest>();
        }
    }
}
