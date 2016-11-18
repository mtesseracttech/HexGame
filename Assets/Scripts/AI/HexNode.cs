using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Saving;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class HexNode
    {
        private Color          _color;
        private HexCoordinates _coordinates;
        private int            _elevation;
        private bool           _hasRiver;
        private bool           _hasRoads;
        private bool           _isUnderWater;
        private bool           _isWalled;
        private Vector3        _position;
        private HexNode[]      _neighbors;
        private int            _index;
        private HexNode        _parent;


        public float CostCurrent;
        public float CostEstimate;


        public HexNode(HexCellInfoContainer info)
        {
            _color        = info.Color;
            _coordinates  = new HexCoordinates((int)info.Coordinates.x, (int)info.Coordinates.z);
            _elevation    = info.Elevation;
            _hasRiver     = info.HasRiver;
            _hasRoads     = info.HasRoads;
            _isUnderWater = info.IsUnderWater;
            _isWalled     = info.IsWalled;
            _position     = info.Position;
            _index        = info.Index;
        }

        public HexNode[] Neighbors
        {
            get { return _neighbors; }
            set { _neighbors = value; }
        }

        public void AddNeighbor(HexNode newNeighbor)
        {
            List<HexNode> neighbors = _neighbors.ToList();
            neighbors.Add(newNeighbor);
            _neighbors = neighbors.ToArray();
        }

        public HexNode Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public HexCoordinates Coordinates
        {
            get { return _coordinates; }
        }

        public Vector3 GetPosition()
        {
            return _position;
        }

        public bool HasRiver
        {
            get { return _hasRiver; }
        }

        public bool HasRoads
        {
            get { return _hasRoads; }
        }

        public bool IsUnderWater
        {
            get { return _isUnderWater; }
        }

        public bool IsWalled
        {
            get { return _isWalled; }
        }

        public int Elevation
        {
            get { return _elevation; }
        }
    }
}