using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class NavigationNode
    {
        private HexCell _cell;
        private HexCell[] _validNeighbors;
        private float[] _neighborCosts;
        private int _elevation;

        public NavigationNode(HexCell cell)
        {
            _cell = cell;
            _elevation = _cell.Elevation;
            SetValidNeighbors();
            CalculateNeighborCosts();
        }

        private void CalculateNeighborCosts()
        {
            _neighborCosts = new float[_validNeighbors.Length];
            for (int i = 0; i < _validNeighbors.Length; i++)
            {
                _neighborCosts[i] = Mathf.Abs(_validNeighbors[i].Elevation - _cell.Elevation);
            }
        }

        public void SetValidNeighbors()
        {
            List<HexCell> neighbors = new List<HexCell>();
//            foreach (var neighbor in _cell.GetNeighbors())
//            {
//                if (Mathf.Abs(neighbor.Elevation - _cell.Elevation) > 2) continue;
//                neighbors.Add(neighbor);
//            }
            _validNeighbors = neighbors.ToArray();

        }

        public HexCell GetCell()
        {
            return _cell;
        }

        public HexCell[] GetValidNeighbors()
        {
            return _validNeighbors;
        }

        public int GetElevation()
        {
            return _elevation;
        }


    }
}