using Leopotam.Ecs;
using UnityEngine;


sealed class EcsStartup : MonoBehaviour
{
    EcsWorld _world;
    EcsSystems _systems;
    EcsSystems _fixedSystems;
    public StaticData staticData;
    public SceneData sceneData;
    public UIData uiData;

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
            .Add(new WheelsUpdateSystem())
            .Add(new CameraSwitchSystem())
            .Add(new DrawPathSystem())
            .Add(new ViewCameraSystem())
            .Add(new DestroyRoadSystem())
            .Add(new HideUIElementsSystem())
            .Add(new InfoPanelSwitchSystem())
            .Add(new BuySystem())
            .Add(new SellSystem())
            .Add(new FactoryProduceSystem())

            // register one-frame components (order is important), for example:
            // .OneFrame<TestComponent1> ()
            // .OneFrame<TestComponent2> ()

            // inject service instances here (order doesn't important), for example:
            // .Inject (new CameraService ())
            // .Inject (new NavMeshSupport ())
            .Inject(staticData)
            .Inject(sceneData)
            .Inject(uiData)
            .Init();
        _fixedSystems
        .Add(new PlayerMoveSystem())
        .Add(new DamageSystem())
        .Add(new FuelSystem())
        .Add(new ImmobilizeSystem())
        .Add(new FarmProduceSystem())




        .Inject(staticData)
        .Inject(sceneData)
        .Inject(uiData)
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
