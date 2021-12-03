using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;


sealed class TrailFollowSystem : IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
{

    EcsFilter<Player> playerFilter;
    SceneData sceneData;
    StaticData staticData;
    List<Transform> activeWeelsTFs = new List<Transform>();
    public void Init()
    {
        foreach (var wc in playerFilter.Get1(0).activeWheelColliders)
        {
            activeWeelsTFs.Add(wc.transform);
        }
        CarReturnBtns.returnEvent += ClearTrails;
    }


    public void Destroy()
    {
        CarReturnBtns.returnEvent -= ClearTrails;
    }

    void IEcsRunSystem.Run()
    {
        ref var player = ref playerFilter.Get1(0);

        for (int i = 0; i < player.carData.wheelDatas.Count; i++)
        {
            float offset = player.activeWheelColliders[i].radius * activeWeelsTFs[i].localScale.y * .8f;
            Vector3 wheelPos;
            Quaternion quaternion;
            player.activeWheelColliders[i].GetWorldPose(out wheelPos, out quaternion);
            wheelPos.y -= offset;

            if (!player.activeWheelColliders[i].isGrounded)
            {
                player.carData.wheelDatas[i].trailTF = null;
                continue;
            }
            else if (!player.carData.wheelDatas[i].trailTF && player.playerRB.velocity.magnitude > .5f)
            {
                player.carData.wheelDatas[i].trailTF = GameObject.Instantiate(staticData.trailPrefab, wheelPos, staticData.trailPrefab.transform.rotation).transform;
            }

            if (player.carData.wheelDatas[i].trailTF)
            {
                player.carData.wheelDatas[i].trailTF.position = wheelPos;
            }
        }
    }

    void ClearTrails()
    {
        ref var player = ref playerFilter.Get1(0);
        for (int i = 0; i < player.carData.wheelDatas.Count; i++)
        {
            player.carData.wheelDatas[i].trailTF = null;
        }
    }


}



