using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] SceneData sceneData;
    int currentAnimalIndex = 0;

    void Start()
    {
        //StartCoroutine(SpawnNextAnimal());
    }

    IEnumerator SpawnNextAnimal()
    {
        yield return new WaitForSeconds(1);
        sceneData.animalsPool[currentAnimalIndex].Revive();
        currentAnimalIndex++;
        if (currentAnimalIndex >= sceneData.animalsPool.Count) yield break;

        StartCoroutine(SpawnNextAnimal());
    }

}
