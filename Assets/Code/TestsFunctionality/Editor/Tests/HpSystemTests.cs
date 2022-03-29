using Code.GameCode.Components;
using Code.GameCode.Systems;
using Code.SupportingCode;
using Leopotam.Ecs;
using NUnit.Framework;

namespace Code
{
    public class HpSystemTests
    {
        private TestsEcsStartUp _ecsStartup = new();
        
        [Test]
        public void add_1_hp_to_not_full_heals_by_1()
        {
            //arrange
            var world = ArrangeHpSystem();

            var entity = world.NewEntity();
            entity.Get<HpComponent>() = new HpComponent
            {
                CurrentValue = 4,
                MaxValue = 5
            };
            
            //act
            entity.Get<ChangeHpSignal>().Value = +1;
            _ecsStartup.Update();
            
            //assert
            Assert.AreEqual(5, entity.Get<HpComponent>().CurrentValue);
            //cleanup
            _ecsStartup.Cleanup();
        }
        
        [Test]
        public void subtracts_1_hp_from_full_damages_by_1()
        {
            //arrange
            var world = ArrangeHpSystem();

            var entity = world.NewEntity();
            entity.Get<HpComponent>() = new HpComponent()
            {
                CurrentValue = 5,
                MaxValue = 5
            };
            
            //act
            entity.Get<ChangeHpSignal>().Value = -1;
            _ecsStartup.Update();
            
            //assert
            Assert.AreEqual(4, entity.Get<HpComponent>().CurrentValue);
            //cleanup
            _ecsStartup.Cleanup();
        }

        [Test]
        public void subtract_full_hp_makes_dead()
        {
            //arrange
            var world = ArrangeHpSystem();

            var entity = world.NewEntity();
            entity.Get<HpComponent>() = new HpComponent()
            {
                MaxValue = 5,
                CurrentValue = 5
            };
            
            //act
            entity.Get<ChangeHpSignal>().Value = -entity.Get<HpComponent>().CurrentValue;;
            _ecsStartup.Update();
            
            //assert
            
            Assert.AreEqual(true, entity.Has<DeadTag>(), $"HP: {entity.Get<HpComponent>().CurrentValue}");
            
            //cleanup
            _ecsStartup.Cleanup();
        }
        
        [Test]
        public void add_1_hp_to_full_not_healing()
        {
            //arrange
            var world = ArrangeHpSystem();

            var entity = world.NewEntity();
            entity.Get<HpComponent>() = new HpComponent()
            {
                MaxValue = 5,
                CurrentValue = 5
            };
            
            //act
            entity.Get<ChangeHpSignal>().Value = 1;
            _ecsStartup.Update();
            
            //assert
            
            Assert.AreEqual(5,5);
            
            //cleanup
            _ecsStartup.Cleanup();
        }

        private EcsWorld ArrangeHpSystem()
        {
            var world = new EcsWorld();
            var systems = new EcsSystems(world);
            var emdFrame = new EcsSystems(world);
            
            systems.Add(new HpSystem());
            emdFrame.OneFrame<ChangeHpSignal>();
            _ecsStartup.Create(world, systems, emdFrame, null, null);
            return world;
        }
    }
}
