using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


sealed class DamageSystem : IEcsRunSystem
{

    EcsFilter<PlayerComp, MovableComp> playerFilter;
    SceneData sceneData;
    UIData uiData;
    GameSettings settings;
    ParticleSystem.EmissionModule emissionModule;
    ParticleSystem.MainModule mainModule;

    void IEcsRunSystem.Run()
    {
        foreach (var fPlayer in playerFilter)
        {
            ref var player = ref playerFilter.Get1(fPlayer);
            foreach (var wheelData in player.carData.wheelDatas)
            {
                if (!wheelData.onRoad && player.playerRB.velocity.magnitude > 1.5f)
                {
                    emissionModule = wheelData.particles.emission;
                    emissionModule.rateOverTime = player.playerRB.velocity.magnitude * 20;
                    mainModule = wheelData.particles.main;
                    mainModule.startSpeed = player.playerRB.velocity.magnitude / 5;
                    wheelData.particles.Play();


                    if (settings.vibration) Handheld.Vibrate();

                    player.currentDurability -= 0.01f * player.playerRB.velocity.magnitude * player.playerRB.mass / 1000;
                    uiData.durabilityText.text = player.currentDurability.ToString("#");
                }
                else
                {
                    wheelData.particles.Stop();
                }
                if (wheelData.inWater)
                {
                    player.currentDurability -= 0.05f;
                    uiData.durabilityText.text = player.currentDurability.ToString("#");
                }
            }
            if (player.currentDurability < 0)
            {
                player.currentDurability = 0;
                sceneData.Notification("Wheels are broken");
                playerFilter.GetEntity(fPlayer).Get<ImmobilizeRequest>();
            }
        }
    }
}
