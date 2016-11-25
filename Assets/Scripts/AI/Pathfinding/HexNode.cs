using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Saving;
using UnityEngine;

namespace Assets.Scripts.AI.Pathfinding
{
    public class HexNode : IComparable<HexNode>
    {
        //A* Related
        public  float          CostCurrent      = 0; //keep track of cost up to now
        public  float          CostEstimate     = 0; //keep track of cost estimate to goal
        public  float          CostCombined     = 0; //The above two added together
        private HexNode        _parent;
        private HexNode[]      _neighbors;

        //Ineteraction Related
        private HexNodeObject  _gameObject;
        private NodeOccupant   _occupant;

        //Original mesh related information
        private Color          _color;
        private HexCoordinates _coordinates;
        private Vector3        _position;
        private int            _elevation;
        private bool           _hasRiver;
        private bool           _hasRoads;
        private bool           _isUnderWater;
        private bool           _isWalled;
        private int            _index;
		private int            _expansion;

        
        //Constructor from the JSON info
        public HexNode(HexCellInfoContainer info)
        {
            _color        = info.Color;
            _coordinates  = new HexCoordinates((int) info.Coordinates.x, (int) info.Coordinates.z);
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
            get { return  _neighbors; }
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
            get { return  _parent; }
            set { _parent = value; }
        }

        public HexCoordinates Coordinates
        {
            get { return _coordinates; }
        }

        public Vector3 Position
        {
            get { return _position; }
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

        public int Index
        {
            get { return _index; }
        }

        public int CompareTo(HexNode other)
        {
            return (CostCombined.CompareTo(other.CostCombined));
        }

		public int Expansion
		{
			get { return  _expansion; }
			set { _expansion = value; }
		}


        public override string ToString()
        {
            string neighborIndexes = "";
            if (_neighbors != null)
            {
                for (int index = 0; index < _neighbors.Length; index++)
                {
                    var neighbor = _neighbors[index];
                    if (index != 0) neighborIndexes += ",";
                    neighborIndexes += neighbor.Index;
                }
            }
            string parent = "";
            if (_parent != null) parent = Parent.Index.ToString();

            string returnString = (
                "Node Info:\n" +
                "Hex Coordinates: " + _coordinates + "\n" +
                "Parent Index: " + parent + "\n" +
                "Neighbors Indexes: " + neighborIndexes
            );
            return returnString;
        }

        public NodeOccupant Occupant
        {
            get { return  _occupant; }
            set
            {
                if (_occupant == null) _occupant = value;
                else{ Debug.Log("You tried to move to an occupied node!, Use HasOccupant to prevent this!"); }
            }
        }

        public bool HasOccupant
        {
            get { return _occupant != null; }
        }

        public bool IsOccupiedByAnything()
        {
            return _hasRiver || _isUnderWater || HasOccupant;
        }
    }
}