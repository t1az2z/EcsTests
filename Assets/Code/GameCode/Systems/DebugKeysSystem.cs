using Code.GameCode.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.GameCode.Systems
{
    public class DebugKeysSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HpComponent> _hp = null;
        private readonly EcsWorld _world = null;
        
        public void Run()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (_hp.IsEmpty())
                {
                    _world.NewEntity().Get<HpComponent>().CurrentValue = 3;
                }
                
                foreach (var index in _hp)
                {
                    _hp.GetEntity(index).Get<ChangeHpSignal>().Value = -1;
                }
            }
        }
    }
}