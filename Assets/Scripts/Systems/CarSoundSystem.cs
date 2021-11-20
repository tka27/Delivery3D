using Leopotam.Ecs;
using UnityEngine;


sealed class CarSoundSystem : IEcsRunSystem
{

    EcsFilter<PlayerComp> filter;
    SceneData sceneData;

    void IEcsRunSystem.Run()
    {
        var player = filter.Get1(0);
        if (player.playerRB.velocity.magnitude < 10)
        {
            player.carData.engineSound.pitch = player.carData.enginePitchDefault + (player.playerRB.velocity.magnitude / 10);
        }
        else
        {
            player.carData.engineSound.pitch = player.carData.enginePitchDefault + .5f + (player.playerRB.velocity.magnitude % 5 / 10);
        }
    }
}



