using UnityEngine;

namespace SeaBattle
{
    [CreateAssetMenu(menuName = "Configurations", fileName = "MainConfiguration", order = 0)]
    public sealed class Configuration : ScriptableObject
    {
        [Header("Grid config")] 
        public Vector3 startCellPosition;
        public Vector2Int gridSize;
        public Vector2Int cellOffset;
    }
}