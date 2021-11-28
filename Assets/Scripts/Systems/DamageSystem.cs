using Leopotam.Ecs;
using UnityEngine;


sealed class DamageSystem : IEcsRunSystem
{

    EcsFilter<Player, Movable> playerFilter;
    SceneData sceneData;
    UIData uiData;
    GameSettings settings;
    ParticleSystem.EmissionModule emissionModule;
    ParticleSystem.MainModule mainModule;

    void IEcsRunSystem.Run()
    {
        ref var player = ref playerFilter.Get1(0);
        foreach (var wheelData in player.carData.wheelDatas)
        {
            if (!wheelData.onRoad && player.playerRB.velocity.magnitude > 1.5f)
            {
                emissionModule = wheelData.particles.emission;
                emissionModule.rateOverTime = player.carData.trailerRB.velocity.magnitude * 20;
                mainModule = wheelData.particles.main;
                mainModule.startSpeed = player.carData.trailerRB.velocity.magnitude / 5;
                wheelData.particles.Play();


                if (settings.vibration) Handheld.Vibrate();

                player.currentDurability -= 0.005f * player.carData.trailerRB.velocity.magnitude * player.playerRB.mass / 1000;
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
            playerFilter.GetEntity(0).Get<ImmobilizeRequest>();
        }
    }
}
