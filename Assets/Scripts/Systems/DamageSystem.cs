using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


sealed class DamageSystem : IEcsRunSystem
{

    EcsFilter<PlayerComp, MovableComp> playerFilter;
    SceneData sceneData;
    UIData uiData;

    void IEcsRunSystem.Run()
    {
        foreach (var fPlayer in playerFilter)
        {
            ref var player = ref playerFilter.Get1(fPlayer);
            foreach (var check in player.playerData.wheelDatas)
            {
                if (!check.onRoad && sceneData.gameMode == GameMode.Drive)
                {
                    player.currentDurability -= 0.05f * player.playerRB.velocity.magnitude;
                    uiData.durabilityText.text = player.currentDurability.ToString("#");
                }
            }
            if (player.currentDurability < 0)
            {
                player.currentDurability = 0;
                uiData.durabilityText.text = "Wheels are broken";
                playerFilter.GetEntity(fPlayer).Get<ImmobilizeRequest>();
            }
        }
    }
}
