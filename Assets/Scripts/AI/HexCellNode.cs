using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Saving
{
    [Serializable]
    public class HexCellNode
    {
        public Color Color;
        public Vector3 Coordinates;
        public int Elevation;
        public bool HasRiver;
        public bool HasRoads;
        public bool IsUnderWater;
        public bool IsWalled;
        public Vector3 Position;
        public int[] NeighborIndexes;
        public int Index;

        public HexCellNode(HexCell cell, int index, List<HexCell> totalCellList)
        {
            Color = cell.Color;
            Coordinates = new Vector3(cell.coordinates.X, cell.coordinates.Y, cell.coordinates.Z);
            Elevation = cell.Elevation;
            HasRiver = cell.HasRiver;
            HasRoads = cell.HasRoads;
            IsUnderWater = cell.IsUnderwater;
            IsWalled = cell.Walled;
            Position = cell.Position;
            Index = index;

            List<int> neighborIndexes = new List<int>();

            HexDirection direction = HexDirection.NE;
            do
            {
                HexCell neighbor = cell.GetNeighbor(direction);
                if (neighbor != null)
                {
                    int neighborIndex = totalCellList.IndexOf(neighbor);
                    neighborIndexes.Add(neighborIndex);
                }
                direction = direction.Next();
            }
            while (direction != HexDirection.NE);

            NeighborIndexes = neighborIndexes.ToArray();

        }

        public override string ToString()
        {
            string neighbors = "";
            foreach (var neighborIndex in NeighborIndexes) neighbors += neighborIndex + "\n";
            return (
                "HexCellNode " + Index + " info:\n" +
                "Color - " + Color + "\n" +
                "HexCoordinates - " + Coordinates + "\n"+
                "Elevation - " + Elevation + "\n" +
                "Contains River - " + HasRiver + "\n" +
                "Contains Road -  " + HasRoads + "\n" +
                "Is Underwater - " + IsUnderWater + "\n" +
                "Is Walled - " + IsWalled + "\n" +
                "Position - " + Position + "\n" +
                "Index - " + Index + "\n" +
                "Neighbor Indexes - \n" + neighbors
            );
        }
    }
}