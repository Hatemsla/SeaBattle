using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace SeaBattle
{
    public sealed class GridInitSystem : IEcsInitSystem
    {
        private readonly EcsPoolInject<CellComp> _cellPool = default;

        private readonly EcsCustomInject<SceneData> _sd = default;
        private readonly EcsCustomInject<Configuration> _cf = default;
        
        public void Init(IEcsSystems systems)
        {
            var world = _cellPool.Value.GetWorld();
            var cellPosition = _cf.Value.startCellPosition;
            _sd.Value.cells = new List<List<CellView>>();
            for (var i = 0; i < _cf.Value.gridSize.y; i++)
            {
                var cellViews = new List<CellView>();
                for (var j = 0; j < _cf.Value.gridSize.x; j++)
                {
                    var cellEntity = world.NewEntity();
                    ref var cell = ref _cellPool.Value.Add(cellEntity);

                    var cellView = Object.Instantiate(_sd.Value.cellPrefab, cellPosition, Quaternion.identity, _sd.Value.cellParent);

                    cell.Position = new Vector2Int(i, j);
                    cell.CellView = cellView;
                    
                    cellPosition.x += _cf.Value.cellOffset.x;
                    
                    cellViews.Add(cellView);
                }

                cellPosition.x = _cf.Value.startCellPosition.x;
                cellPosition.z -= _cf.Value.cellOffset.y;
                
                _sd.Value.cells.Add(cellViews);
            }
        }
    }
}