using System.Collections.Generic;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using UnityEngine.Scripting;

namespace SeaBattle
{
    public sealed class ShipsRandomPlaceSystem : EcsUguiCallbackSystem
    {
        private readonly EcsFilterInject<Inc<ShipComp>> _shipFilter = default;
        
        private readonly EcsCustomInject<SceneData> _sd = default;
        private readonly EcsCustomInject<Configuration> _cf = default;
        
        [Preserve]
        [EcsUguiClickEvent(Idents.UI.AutoPlace, Idents.Worlds.Events)]
        private void OnAutoPlacedClick(in EcsUguiClickEvent e)
        {
            UnplacedCells();
            
            foreach (var entity in _shipFilter.Value)
            {
                ref var ship = ref _shipFilter.Pools.Inc1.Get(entity);

                PlaceShip(ship);
            }
        }

        private void PlaceShip(ShipComp ship)
        {
            if (ship.ShipView.size == 4)
            {
                var isPlaced = false;
                while (!isPlaced)
                {
                    var randXPos = Random.Range(0, _cf.Value.gridSize.x);
                    var randZPos = Random.Range(0, _cf.Value.gridSize.y);
                    var isVertical = Random.Range(0, 2) == 1;

                    if (isVertical && randXPos - 3 >= 0)
                    {
                        var firstRandCell = _sd.Value.cells[randXPos][randZPos];
                        var secondRandCell = _sd.Value.cells[randXPos - 1][randZPos];
                        var thirdRandCell = _sd.Value.cells[randXPos - 2][randZPos];
                        var fourthRandCell = _sd.Value.cells[randXPos - 3][randZPos];

                        if (!firstRandCell.isPlaced && !secondRandCell.isPlaced && !thirdRandCell.isPlaced &&
                            !fourthRandCell.isPlaced &&
                            AroundNeighborCellsEmpty(randXPos, randZPos) &&
                            AroundNeighborCellsEmpty(randXPos - 1, randZPos) &&
                            AroundNeighborCellsEmpty(randXPos - 2, randZPos) &&
                            AroundNeighborCellsEmpty(randXPos - 3, randZPos))
                        {
                            var cellPos = (firstRandCell.transform.position + secondRandCell.transform.position +
                                           thirdRandCell.transform.position + fourthRandCell.transform.position) / 4;
                            ship.ShipView.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                            ship.ShipView.transform.position = new Vector3(cellPos.x, 0, cellPos.z);

                            MarkOccupiedCells(randXPos, randZPos);
                            MarkOccupiedCells(randXPos - 1, randZPos);
                            MarkOccupiedCells(randXPos - 2, randZPos);
                            MarkOccupiedCells(randXPos - 3, randZPos);

                            isPlaced = true;
                        }
                    }
                    else if (!isVertical && randZPos - 3 >= 0)
                    {
                        var firstRandCell = _sd.Value.cells[randXPos][randZPos];
                        var secondRandCell = _sd.Value.cells[randXPos][randZPos - 1];
                        var thirdRandCell = _sd.Value.cells[randXPos][randZPos - 2];
                        var fourthRandCell = _sd.Value.cells[randXPos][randZPos - 3];

                        if (!firstRandCell.isPlaced && !secondRandCell.isPlaced && !thirdRandCell.isPlaced &&
                            !fourthRandCell.isPlaced &&
                            AroundNeighborCellsEmpty(randXPos, randZPos) &&
                            AroundNeighborCellsEmpty(randXPos, randZPos - 1) &&
                            AroundNeighborCellsEmpty(randXPos, randZPos - 2) &&
                            AroundNeighborCellsEmpty(randXPos, randZPos - 3))
                        {
                            var cellPos = (firstRandCell.transform.position + secondRandCell.transform.position +
                                           thirdRandCell.transform.position + fourthRandCell.transform.position) / 4;
                            ship.ShipView.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                            ship.ShipView.transform.position = new Vector3(cellPos.x, 0, cellPos.z);

                            MarkOccupiedCells(randXPos, randZPos);
                            MarkOccupiedCells(randXPos, randZPos - 1);
                            MarkOccupiedCells(randXPos, randZPos - 2);
                            MarkOccupiedCells(randXPos, randZPos - 3);

                            isPlaced = true;
                        }
                    }
                }
            }


            if (ship.ShipView.size == 3)
            {
                var isPlaced = false;
                while (!isPlaced)
                {
                    var randXPos = Random.Range(0, _cf.Value.gridSize.x);
                    var randZPos = Random.Range(0, _cf.Value.gridSize.y);
                    var isVertical = Random.Range(0, 2) == 1;

                    if (isVertical && randXPos - 2 >= 0)
                    {
                        var firstRandCell = _sd.Value.cells[randXPos][randZPos];
                        var secondRandCell = _sd.Value.cells[randXPos - 1][randZPos];
                        var thirdRandCell = _sd.Value.cells[randXPos - 2][randZPos];

                        if (!firstRandCell.isPlaced && !secondRandCell.isPlaced && !thirdRandCell.isPlaced &&
                            AroundNeighborCellsEmpty(randXPos, randZPos) &&
                            AroundNeighborCellsEmpty(randXPos - 1, randZPos) &&
                            AroundNeighborCellsEmpty(randXPos - 2, randZPos))
                        {
                            var cellPos = (firstRandCell.transform.position + secondRandCell.transform.position +
                                           thirdRandCell.transform.position) / 3;
                            ship.ShipView.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                            ship.ShipView.transform.position = new Vector3(cellPos.x, 0, cellPos.z);

                            MarkOccupiedCells(randXPos, randZPos);
                            MarkOccupiedCells(randXPos - 1, randZPos);
                            MarkOccupiedCells(randXPos - 2, randZPos);

                            isPlaced = true;
                        }
                    }
                    else if (!isVertical && randZPos - 2 >= 0)
                    {
                        var firstRandCell = _sd.Value.cells[randXPos][randZPos];
                        var secondRandCell = _sd.Value.cells[randXPos][randZPos - 1];
                        var thirdRandCell = _sd.Value.cells[randXPos][randZPos - 2];

                        if (!firstRandCell.isPlaced && !secondRandCell.isPlaced && !thirdRandCell.isPlaced &&
                            AroundNeighborCellsEmpty(randXPos, randZPos) &&
                            AroundNeighborCellsEmpty(randXPos, randZPos - 1) &&
                            AroundNeighborCellsEmpty(randXPos, randZPos - 2))
                        {
                            var cellPos = (firstRandCell.transform.position + secondRandCell.transform.position +
                                           thirdRandCell.transform.position) / 3;
                            ship.ShipView.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                            ship.ShipView.transform.position = new Vector3(cellPos.x, 0, cellPos.z);

                            MarkOccupiedCells(randXPos, randZPos);
                            MarkOccupiedCells(randXPos, randZPos - 1);
                            MarkOccupiedCells(randXPos, randZPos - 2);

                            isPlaced = true;
                        }
                    }
                }
            }

            if (ship.ShipView.size == 2)
            {
                var isPlaced = false;
                while (!isPlaced)
                {
                    var randXPos = Random.Range(0, _cf.Value.gridSize.x);
                    var randZPos = Random.Range(0, _cf.Value.gridSize.y);

                    var isVertical = Random.Range(0, 2) == 1;

                    if (isVertical && randXPos - 1 >= 0)
                    {
                        var firstRandCell = _sd.Value.cells[randXPos][randZPos];
                        var secondRandCell = _sd.Value.cells[randXPos - 1][randZPos];

                        if (!firstRandCell.isPlaced && !secondRandCell.isPlaced &&
                            AroundNeighborCellsEmpty(randXPos, randZPos) &&
                            AroundNeighborCellsEmpty(randXPos - 1, randZPos))
                        {
                            var cellPos = (firstRandCell.transform.position + secondRandCell.transform.position) / 2;
                            ship.ShipView.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                            ship.ShipView.transform.position = new Vector3(cellPos.x, 0, cellPos.z);

                            MarkOccupiedCells(randXPos, randZPos);
                            MarkOccupiedCells(randXPos - 1, randZPos);

                            isPlaced = true;
                        }
                    }
                    else if (!isVertical && randZPos - 1 >= 0)
                    {
                        var firstRandCell = _sd.Value.cells[randXPos][randZPos];
                        var secondRandCell = _sd.Value.cells[randXPos][randZPos - 1];

                        if (!firstRandCell.isPlaced && !secondRandCell.isPlaced &&
                            AroundNeighborCellsEmpty(randXPos, randZPos) &&
                            AroundNeighborCellsEmpty(randXPos, randZPos - 1))
                        {
                            var cellPos = (firstRandCell.transform.position + secondRandCell.transform.position) / 2;
                            ship.ShipView.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                            ship.ShipView.transform.position = new Vector3(cellPos.x, 0, cellPos.z);

                            MarkOccupiedCells(randXPos, randZPos);
                            MarkOccupiedCells(randXPos, randZPos - 1);

                            isPlaced = true;
                        }
                    }
                }
            }

            if (ship.ShipView.size == 1)
            {
                var isPlaced = false;
                while (!isPlaced)
                {
                    var randXPos = Random.Range(0, _cf.Value.gridSize.x);
                    var randZPos = Random.Range(0, _cf.Value.gridSize.y);

                    var randCell = _sd.Value.cells[randXPos][randZPos];
                    if (!randCell.isPlaced && AroundNeighborCellsEmpty(randXPos, randZPos))
                    {
                        var cellPos = randCell.transform.position;
                        ship.ShipView.transform.position = new Vector3(cellPos.x, 0, cellPos.z);

                        MarkOccupiedCells(randXPos, randZPos);

                        isPlaced = true;
                    }
                }
            }
        }

        private void UnplacedCells()
        {
            foreach (var cells in _sd.Value.cells)
            {
                foreach (var cell in cells)
                {
                    cell.isPlaced = false;
                    cell.isShip = false;
                    cell.Unplaced();
                }
            }
        }

        private void MarkOccupiedCells(int x, int z)
        {
            for (var i = x - 1; i <= x + 1; i++)
            {
                for (var j = z - 1; j <= z + 1; j++)
                {
                    if (i >= 0 && i < _cf.Value.gridSize.x && j >= 0 && j < _cf.Value.gridSize.y)
                    {
                        if (i == x && j == z)
                            _sd.Value.cells[i][j].isShip = true;
                            
                        _sd.Value.cells[i][j].isPlaced = true;
                        _sd.Value.cells[i][j].Placed();
                    }
                }
            }
        }

        private bool AroundNeighborCellsEmpty(int x, int z)
        {
            for (var i = x - 1; i <= x + 1; i++)
            {
                for (var j = z - 1; j <= z + 1; j++)
                {
                    if (i >= 0 && i < _cf.Value.gridSize.x && j >= 0 && j < _cf.Value.gridSize.y)
                    {
                        if (_sd.Value.cells[i][j].isShip)
                            return false;

                        if (i == x && j == z && _sd.Value.cells[i][j].isPlaced)
                            return false;
                    }
                }
            }
            return true;
        }
    }
}