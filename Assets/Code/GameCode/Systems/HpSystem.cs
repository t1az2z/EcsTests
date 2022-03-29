using Code.GameCode.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.GameCode.Systems
{
    public class HpSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HpComponent, ChangeHpSignal>.Exclude<DeadTag> _hpToChange = null;
        
        public void Run()
        {
            foreach (var index in _hpToChange)
            {
                ref var hp = ref _hpToChange.Get1(index);
                hp.CurrentValue = Mathf.Clamp(hp.CurrentValue + _hpToChange.Get2(index).Value, 0, hp.MaxValue);
                
                if (hp.CurrentValue <= 0)
                    _hpToChange.GetEntity(index).Get<DeadTag>();
            }    
        }
    }
}