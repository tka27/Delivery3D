using Leopotam.Ecs;
using UnityEngine;


sealed class EcsStartup : MonoBehaviour
{
    EcsWorld _world;
    EcsSystems _systems;
    EcsSystems _fixedSystems;
    public StaticData staticData;
    public SceneData sceneData;

    void Start()
    {
        // void can be switched to IEnumerator for support coroutines.
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        _fixedSystems = new EcsSystems(_world);
#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_fixedSystems);
#endif
        _systems
            .Add(new GameInitSystem())
            .Add(new DrawPathSystem())

            // register one-frame components (order is important), for example:
            // .OneFrame<TestComponent1> ()
            // .OneFrame<TestComponent2> ()

            // inject service instances here (order doesn't important), for example:
            // .Inject (new CameraService ())
            // .Inject (new NavMeshSupport ())
            .Inject(staticData)
            .Inject(sceneData)
            .Init();
        _fixedSystems
        .Add(new PlayerMoveSystem())




        .Inject(staticData)
        .Inject(sceneData)
        .Init();
    }

    void Update()
    {
        _systems?.Run();
    }
    void FixedUpdate()
    {
        _fixedSystems?.Run();
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
