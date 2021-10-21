using Leopotam.Ecs;
using UnityEngine;


sealed class EcsStartup : MonoBehaviour
{
    EcsWorld _world;
    EcsSystems _systems;
    public StaticData staticData;
    public SceneData sceneData;

    void Start()
    {
        // void can be switched to IEnumerator for support coroutines.

        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        RuntimeData runtimeData = new RuntimeData();
#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
        _systems
            .Add(new GameInitSystem())
            .Add(new InputSystem())
            .Add(new DrawPathSystem())
            .Add(new PlayerMoveSystem())

            // register one-frame components (order is important), for example:
            // .OneFrame<TestComponent1> ()
            // .OneFrame<TestComponent2> ()

            // inject service instances here (order doesn't important), for example:
            // .Inject (new CameraService ())
            // .Inject (new NavMeshSupport ())
            .Inject(staticData)
            .Inject(sceneData)
            .Inject(runtimeData)
            .Init();
    }

    void Update()
    {
        _systems?.Run();
    }

    void OnDestroy()
    {
        if (_systems != null)
        {
            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }
}
