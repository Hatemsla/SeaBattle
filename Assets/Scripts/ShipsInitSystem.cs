using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SeaBattle
{
    public sealed class ShipsInitSystem : IEcsInitSystem
    {
        private readonly EcsPoolInject<ShipComp> _shipPool = default;

        private readonly EcsCustomInject<SceneData> _sd = default;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            foreach (var ship in _sd.Value.ships)
            {
                var shipEntity = world.NewEntity();
                ref var shipComp = ref _shipPool.Value.Add(shipEntity);

                shipComp.ShipView = ship;
            }
        }
    }
}