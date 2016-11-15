using System;
using UnityEngine;
using System.Collections.Generic;

public class NodeManager : MonoBehaviour
{
    public HexGrid HexGrid;
    private HexCell[] _hexCells;
    private List<Vector3> _vectors;
    private List<HexCell> _frontier;
    private HexCell _current;


    public void CalculateNavigationPath()
    {
    }

    void Start()
    {
<<<<<<< HEAD
        //_hexCells = HexGrid.GetCells();
        _hexCells[0].color = Color.magenta;
        _hexCells[_hexCells.Length - 3].color = Color.cyan;
        GetPath(_hexCells[0], _hexCells[_hexCells.Length - 3]);
       // HexGrid.Refresh();
=======
      //  _hexCells = HexGrid.GetCells();
        _hexCells[0].color = Color.magenta;
        _hexCells[_hexCells.Length - 3].color = Color.cyan;
        GetPath(_hexCells[0], _hexCells[_hexCells.Length - 3]);
        //HexGrid.Refresh();
>>>>>>> f031da5f91853909be64b016693598087bbf5f56
    }

    void Update()
    {

    }

    public void GetPath(HexCell start, HexCell end)
    {
        var frontier = new Queue<HexCell>();
        frontier.Enqueue(start);

        var visited = new HashSet<HexCell>();
        visited.Add(start);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            Debug.Log("Visiting: " + current.transform.position);
//            foreach (var next in current.GetNeighbors())
//            {
//                if (next == null) continue;
//                if (next == end) return;
//                if (!visited.Contains(next)) {
//                    frontier.Enqueue(next);
//                    visited.Add(next);
//                    next.color = Color.blue;
//                }
//            }

        }
    }

    /*
    public List<Vector3> GetPath(HexCell start, HexCell end)
    {
        while (_frontier.Count > 0)
        {
            _frontier.Add(start);
            if (_current == end) break;

            HexDirection direction = HexDirection.NE;
            do
            {
                if (_current.GetNeighbor(direction) != null)
                {

                }
                direction = direction.Next();
            }
            while (direction != HexDirection.NE);
        }
    }
    */


    public void PrintHexCellNeighbours(HexCell input)
    {
        string printString = "HexCell Info:\n";
        printString += input.transform.position;
        printString += "Neighbours:\n";
        //HexCell[] inputNeighbors = input.GetNeighbors();
//        foreach (var hexCell in inputNeighbors)
//        {
//            if (hexCell == null) continue;
//            printString += hexCell.transform.position + "\n";
//        }
       // Debug.Log(printString);
    }
}
