using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Assets;
using Assets.Scripts.AI;

public class AStar
{
    private List<HexNode> _todoList;
    private List<HexNode> _doneList;
    private HexNode       _currentNode;
    private HexNode       _endNode;
    private HexNode       _startNode;
    private List<HexNode> _path;
    private bool          _done             = false;

    public AStar(HexNode start, HexNode end)
    {
        _todoList = new List<HexNode>();
        _doneList = new List<HexNode>();
        _startNode = start;
        _endNode = end;
        _todoList.Add(_startNode);
    }

    public void Search()
    {
        while (!_done)
        {
            Step();
        }
        PaintPath();
    }

    public void Step()
    {
        if (_todoList.Count < 1 || _done)
        {
            _done = true;
            return;
        }

        _todoList.Sort();
        _currentNode = _todoList[0];
        _todoList.RemoveAt(0);
        _doneList.Add(_currentNode);
        //_currentNode.SetColor(Color.red);
        if (_currentNode == _endNode)
        {
            _done = true;
            GeneratePath();
            return;
        }

        foreach (var neighbor in _currentNode.Neighbors)
        {

            if (!_todoList.Contains(neighbor) && !_doneList.Contains(neighbor))
            {
                neighbor.Parent = _currentNode;
                neighbor.CostCurrent = _currentNode.CostCurrent +
                                       Vector2.Distance(_currentNode.GetPosition(), neighbor.GetPosition());
                neighbor.CostEstimate = Vector2.Distance(neighbor.GetPosition(), _endNode.GetPosition());
                _todoList.Add(neighbor);
            }


            /*
            if (_doneList.Contains(neighbor)) continue;
            if (_todoList.Contains(neighbor) || _currentNode.CostEstimate < neighbor.CostEstimate)
            {
                neighbor.SetParent(_currentNode);
                neighbor.CostCurrent = _currentNode.CostCurrent +
                                       Vector2.Distance(_currentNode.GetPosition(), neighbor.GetPosition());
                neighbor.CostEstimate = Vector2.Distance(neighbor.GetPosition(), _endNode.GetPosition());
                if (!_todoList.Contains(neighbor))
                {
                    _todoList.Add(neighbor);
                }
            }
            */

        }

        /*
        foreach (HexNode node in _todoList)
        {
            node.SetColor(Color.blue);
        }
        foreach (var node in _doneList)
        {
            node.SetColor(Color.gray);
        }
        */

    }

    public List<HexNode> GetPath()
    {
        if (_done && _path != null)
        {
            return _path;
        }
        return null;
    }

    public void PaintPath()
    {
        if (_path != null)
        {
            foreach (var node in _path)
            {
                //node.SetColor(Color.black);
            }
        }
    }

    private void GeneratePath()
    {
        HexNode node = _currentNode;
        List<HexNode> nodeList = new List<HexNode>();
        while (node.Parent != null)
        {
            nodeList.Add(node);
            node = node.Parent;
        }
        nodeList.Add(node);
        nodeList.Reverse();
        _path = nodeList;
    }
}
