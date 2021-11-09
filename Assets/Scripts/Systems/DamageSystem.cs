using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


sealed class DamageSystem : IEcsRunSystem
{

    EcsFilter<PlayerComp, MovableComp> playerFilter;
    SceneData sceneData;
    UIData uiData;
    ParticleSystem.EmissionModule emissionModule;
    ParticleSystem.MainModule mainModule;

    void IEcsRunSystem.Run()
    {
        foreach (var fPlayer in playerFilter)
        {
            ref var player = ref playerFilter.Get1(fPlayer);
            foreach (var wheelData in player.playerData.wheelDatas)
            {
                if (!wheelData.onRoad && sceneData.gameMode == GameMode.Drive && player.playerRB.velocity.magnitude > 1.5f)
                {
                    emissionModule = wheelData.particles.emission;
                    emissionModule.rateOverTime = player.playerRB.velocity.magnitude * 10;
                    mainModule = wheelData.particles.main;
                    mainModule.startSpeed = player.playerRB.velocity.magnitude / 5;
                    wheelData.particles.Play();


                    Handheld.Vibrate();
                    player.currentDurability -= 0.02f * player.playerRB.velocity.magnitude*player.playerRB.mass/1000;
                    uiData.durabilityText.text = player.currentDurability.ToString("#");
                }else{
                    wheelData.particles.Stop();
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
