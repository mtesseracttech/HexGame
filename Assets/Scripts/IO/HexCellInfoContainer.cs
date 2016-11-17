using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Saving
{
    [Serializable]
    public class HexCellInfoContainer
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

        public HexCellInfoContainer(HexCell cell, int index)
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
        }

        public override string ToString()
        {
            string neighbors = "";
            foreach (var neighborIndex in NeighborIndexes) neighbors += neighborIndex + "\n";
            return (
                "HexCellInfoContainer " + Index + " info:\n" +
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