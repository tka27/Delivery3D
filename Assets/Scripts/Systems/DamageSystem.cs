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
        foreach (var playerF in playerFilter)
        {
            ref var player = ref playerFilter.Get1(playerF);
            //Debug.Log(player.playerRB.velocity.magnitude);
            foreach (var check in player.playerData.wheelDatas)
            {
                if (!check.onRoad && sceneData.gameMode == GameMode.Drive)
                {
                    player.currentHealth -= 0.05f * player.playerRB.velocity.magnitude;
                    Debug.Log(player.currentHealth);
                }
            }
            if (player.currentHealth < 0)
            {
                player.currentHealth = 0;
                player.currentTorque = 0;
                foreach (var wheel in player.playerData.wheelColliders)
                {
                    wheel.motorTorque = player.currentTorque;
                }
                playerFilter.GetEntity(playerF).Del<MovableComp>();
            }
        }
    }
}
