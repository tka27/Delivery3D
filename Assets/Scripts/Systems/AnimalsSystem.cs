using Leopotam.Ecs;
using UnityEngine;

sealed class AnimalsSystem : IEcsRunSystem
{

    EcsFilter<Animal> animalFilter;
    SceneData sceneData;
    int currentPointIndex;

    void IEcsRunSystem.Run()
    {
        foreach (var animalInd in animalFilter)
        {
            ref var animal = ref animalFilter.Get1(animalInd);
            if (!animal.animalData.isAlive) continue;


            //if (animal.animalData.agent.isOnNavMesh)
            if (animal.animalData.agent.remainingDistance < 1)
            {
                int randomIndex = Random.Range(0, sceneData.animalSpawnPoints.Count);
                currentPointIndex = randomIndex;
                animal.animalData.agent.SetDestination(sceneData.animalSpawnPoints[randomIndex].position);
            }
        }
    }
}



