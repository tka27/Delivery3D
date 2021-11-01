using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


sealed class DamageSystem : IEcsRunSystem
{

    EcsFilter<PlayerComp, MovableComp> playerFilter;
    SceneData sceneData;

    void IEcsRunSystem.Run()
    {
        foreach (var fPlayer in playerFilter)
        {
            ref var player = ref playerFilter.Get1(fPlayer);
            foreach (var check in player.playerData.wheelDatas)
            {
                if (!check.onRoad && sceneData.gameMode == GameMode.Drive)
                {
                    player.currentHealth -= 0.05f * player.playerRB.velocity.magnitude;
                }
            }
            if (player.currentHealth < 0)
            {
                player.currentHealth = 0;
                playerFilter.GetEntity(fPlayer).Get<ImmobilizeRequest>();
            }
        }
    }
}
