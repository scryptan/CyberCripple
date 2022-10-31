using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CyberCripple.Movement
{
    public class MovementSystem : IEcsRunSystem
    {
        private readonly EcsPoolInject<MovementComponent> _movementComponents = default;
        private readonly EcsPoolInject<PositionComponent> _positionComponents = default;

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entities = world
                .Filter<MovementComponent>()
                .Inc<PositionComponent>()
                .End();

            foreach (var entity in entities)
            {
                ref var movement = ref _movementComponents.Value.Get(entity);
                ref var position = ref _positionComponents.Value.Get(entity);

                var startPosition = position.Position.position;
                var targetPosition = movement.Target.position;

                if (Vector3.Distance(startPosition, targetPosition) < 0.1f)
                {
                    _movementComponents.Value.Del(entity);
                    continue;
                }

                position.Position.Translate((targetPosition - startPosition).normalized * movement.Speed *
                                            Time.deltaTime);
            }
        }
    }
}