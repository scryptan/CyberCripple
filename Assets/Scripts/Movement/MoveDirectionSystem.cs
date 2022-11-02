using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CyberCripple.Movement
{
    public class MoveDirectionSystem : IEcsRunSystem
    {
        private readonly EcsPoolInject<PositionComponent> _positionComponents = default;
        private readonly EcsPoolInject<MoveDirectionComponent> _moveDirectionComponents = default;

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entities = world
                .Filter<MoveDirectionComponent>()
                .Inc<PositionComponent>()
                .End();

            foreach (var entity in entities)
            {
                ref var position = ref _positionComponents.Value.Get(entity);
                ref var moveDirectionComponent = ref _moveDirectionComponents.Value.Get(entity);

                position.Position.Translate(moveDirectionComponent.Direction * moveDirectionComponent.Speed *
                                            Time.deltaTime);

                _moveDirectionComponents.Value.Del(entity);
            }
        }
    }
}