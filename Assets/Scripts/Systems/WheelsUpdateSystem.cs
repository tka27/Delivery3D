using Leopotam.Ecs;
using UnityEngine;

sealed class WheelsUpdateSystem : IEcsRunSystem
{
    EcsFilter<PlayerComp> playerFilter;
    void IEcsRunSystem.Run()
    {
        foreach (var fPlayer in playerFilter)
        {
            ref var player = ref playerFilter.Get1(fPlayer);
            for (int i = 0; i < player.playerData.allWheelColliders.Count; i++)
            {
                Vector3 pos;
                Quaternion quaternion;
                player.playerData.allWheelColliders[i].GetWorldPose(out pos, out quaternion);
                player.playerData.allWheelMeshes[i].transform.position = pos;
                player.playerData.allWheelMeshes[i].transform.rotation = quaternion;
            }
        }
    }
}
