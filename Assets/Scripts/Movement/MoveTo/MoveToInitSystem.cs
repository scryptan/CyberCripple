using Leopotam.EcsLite;
using UnityEngine;

namespace CyberCripple.Movement.MoveTo
{
    public class MoveToInitSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var movementMBs = Object.FindObjectsOfType<MoveToMB>();

            var movementComponents = world.GetPool<MoveToComponent>();
            var positionComponents = world.GetPool<PositionComponent>();

            foreach (var movementMb in movementMBs)
            {
                var entity = world.NewEntity();

                ref var movement = ref movementComponents.Add(entity);
                ref var position = ref positionComponents.Add(entity);

                movement.Speed = 5;
                movement.Target = movementMb.Target;
                
                position.Position = movementMb.transform;
            }
        }
    }
}