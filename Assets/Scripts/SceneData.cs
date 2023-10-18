using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeaBattle
{
    public sealed class SceneData : MonoBehaviour
    {
        public Transform cellParent;
        public CellView cellPrefab;
        public List<ShipView> ships;
        public List<List<CellView>> cells;
    }
}