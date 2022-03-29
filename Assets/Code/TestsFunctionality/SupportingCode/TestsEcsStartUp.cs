using Leopotam.Ecs;

namespace Code.SupportingCode
{
    public class TestsEcsStartUp
    {
        private EcsWorld _world;
        private EcsSystems _systems;
        private EcsSystems _fixedUpdateSystems;

        public void Create(EcsWorld world, EcsSystems systems, EcsSystems endFrame, 
            EcsSystems fixedUpdateSystems, EcsSystems fixedUpdateEndFrame)
        {
            _world = world;
            _systems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            
            _systems.Add(systems);
            
            AddDefaultSystems(endFrame);
            _systems.Add(endFrame);

            if (fixedUpdateSystems != null)
                _fixedUpdateSystems.Add(fixedUpdateSystems);
            if (fixedUpdateEndFrame != null)
                _fixedUpdateSystems.Add(fixedUpdateEndFrame);

            _systems
                .Init();

            _fixedUpdateSystems
                .Init();
        }

        public void Update()
        {
            _systems.Run();
        }

        public void FixedUpdate()
        {
            _fixedUpdateSystems.Run();
        }

        public void Cleanup()
        {
            if (_fixedUpdateSystems != null)
            {
                _fixedUpdateSystems.Destroy();
                _fixedUpdateSystems = null;
            }

            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }

        private void AddDefaultSystems(EcsSystems endFrame)
        {
            //Add here default systems for all test cases
            // _systems
            //     .Add(new GameInit())
            //
            //     // event group
            //     .Add(new Modules.EventGroup.StateCleanupSystem()) // remove entity with prev state component
            //     .Add(new OnRestartRoundEnter()) // on click at restart button
            //     .Add(new OnNextLevelEnter()) // start next level
            //     .Add(new OnGamePlayStateEnter()) // enter at gameplay stage
            //     .Add(new OnRoundCompletedEnter()) // on round completed state enter
            //     .Add(new OnRoundFailedEnter()) // on round failed state enter
            //     .Add(new TimedDestructorSystem());
            //
            // endFrame
            //     .OneFrame<Modules.EventGroup.StateEnter>();
        }
    }
}