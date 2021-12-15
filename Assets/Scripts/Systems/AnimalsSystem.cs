using Leopotam.Ecs;
using UnityEngine;

sealed class AnimalsSystem : IEcsRunSystem
{
    EcsFilter<Animal> animalFilter;

    void IEcsRunSystem.Run()
    {
        float count = 0;

        foreach (var animalInd in animalFilter)
        {
            ref var animal = ref animalFilter.Get1(animalInd);
            if (!animal.animalData.isAlive) continue;

            if (animal.animalData.agent.velocity.magnitude < 1)
            {
                count++;
            }

            if (animal.animalData.agent.remainingDistance < 2)
            {
                animal.animalData.SetPath();
            }
        }
        if (count > 0)
        {
            Debug.Log("HUETA: " + count);
        }

    }
}



