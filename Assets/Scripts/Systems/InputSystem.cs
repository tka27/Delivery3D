using Leopotam.Ecs;
using UnityEngine;

sealed class InputSystem : IEcsRunSystem
{

    EcsFilter<InputComp> inputFilter;

    void IEcsRunSystem.Run()
    {
        foreach (var f1 in inputFilter)
        {
            ref var input = ref inputFilter.Get1(f1);
            input.mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15));
            input.mouse0Down = Input.GetMouseButton(0);
            input.spaceDown = Input.GetKey(KeyCode.Space);
        }
    }
}
