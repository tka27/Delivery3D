using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;


sealed class TrailFollowSystem : IEcsRunSystem, IEcsInitSystem
{

    EcsFilter<Player> filter;
    SceneData sceneData;
    List<Transform> trailsTFs;
    int currentTrailIndex;
    public void Init()
    {
        foreach (var trail in sceneData.trails)
        {
            trailsTFs.Add(trail.transform);
        }

        
    }

    void IEcsRunSystem.Run()
    {
        ref var player = ref filter.Get1(0);
        foreach (var wc in player.carData.allWheelColliders)
        {
            if (wc.isGrounded) continue;

        }
        
    }
}



