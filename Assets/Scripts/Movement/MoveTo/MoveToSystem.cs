using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CyberCripple.Movement.MoveTo
{
    public class MoveToSystem : IEcsRunSystem
    {
        private readonly EcsPoolInject<MoveToComponent> _moveToComponents = default;
        private readonly EcsPoolInject<PositionComponent> _positionComponents = default;
        private readonly EcsPoolInject<MoveDirectionComponent> _moveDirectionComponents = default;

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entities = world
                .Filter<MoveToComponent>()
                .Inc<PositionComponent>()
                .Exc<MoveDirectionComponent>()
                .End();

            foreach (var entity in entities)
            {
                ref var movement = ref _moveToComponents.Value.Get(entity);
                ref var position = ref _positionComponents.Value.Get(entity);

                var startPosition = position.Position.position;
                var targetPosition = movement.Target.position;

                if (Vector3.Distance(startPosition, targetPosition) < 0.1f)
                {
                    _moveToComponents.Value.Del(entity);
                    continue;
                }

                ref var moveDirectionComponent = ref _moveDirectionComponents.Value.Add(entity);
                moveDirectionComponent.Direction = (targetPosition - startPosition).normalized;
                moveDirectionComponent.Speed = 5;
            }
        }
    }
}