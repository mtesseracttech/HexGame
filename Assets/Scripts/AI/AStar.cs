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

    public void Search()
    {
        while (!_done)
        {
            Step();
        }
        //PaintPath();
    }

    public bool Step()
    {
        //return false;

        //are we able to find a path??
        if (_done || _startNode == null || _endNode == null || _todoList.Count == 0)
        {
            _done = true;
            return true;
        }

        //we are not done, start and end are set and there is at least 1 item on the open list...

        //check if we were already processing nodes, if so color the last processed node as black because it is on the closed list
        if (_currentNode != null)
        {
            //_currentNode.SetColor(Color.DarkSlateGray);
        }

        //get a node from the open list
        _currentNode = _todoList[0];
        _todoList.RemoveAt(0);

        //_currentNode.SetColor(Color.Orange);

        //and move that node to the closed list (one way or another, we are done with it...)
        _doneList.Add(_currentNode);
        //_currentNode.info = "";

        //is this our node? yay done...
        if (_currentNode == _endNode)
        {
            GeneratePath();
            _done = true;
        }
        else
        {
            //get all children and process them
            for (var i = 0; i < _currentNode.Neighbors.Length; i++)
            {
                HexNode neighbor = _currentNode.Neighbors[i];//_currentNode.GetConnectionAt(i);

                if (_doneList.IndexOf(neighbor) == -1 && _todoList.IndexOf(neighbor) == -1)
                {
                    //connectedNode.parentNode = _currentNode;


                    if (neighbor.CostCurrent < _currentNode.CostCurrent)
                    {
                        neighbor.Parent = _currentNode;
                    }
                    //connectedNode.SetColor(Color.Blue);

                    neighbor.CostCurrent = _currentNode.CostCurrent +
                                                Vector3.Distance(_currentNode.Position, neighbor.Position);//DistanceTo(connectedNode.position);
                    neighbor.CostEstimate = Vector3.Distance(neighbor.Position, _endNode.Position);//connectedNode.Position.DistanceTo(_endNode.position);


                    _todoList.Add(neighbor);
                }
            }

            _todoList.Sort();
            //updateNodeInfo();
        }

        return _done;
    }

    public List<HexNode> GetPath()
    {
        if (_done && _path != null)
        {
            return _path;
        }
        return null;
    }

    public void SetStartNode(HexNode start)
    {
        if (_startNode != null) resetNode(_startNode);
        _startNode = start;
        ResetPathFinder();
    }

    public void SetEndNode(HexNode end)
    {
        if (_endNode != null) resetNode(_endNode);
        _endNode = end;
        ResetPathFinder();
    }


    public bool IsDone()
    {
        return _done;
    }

    public List<HexNode> GetLastFoundPath()
    {
        return _path;
    }

    private void GeneratePath()
    {
        List<HexNode> nodeList = new List<HexNode>();
        HexNode node = _endNode;
        while (node != null)
        {
            nodeList.Add(node);
            node = node.Parent;
        }
        nodeList.Add(node);
        nodeList.Reverse();
        _path = nodeList;
    }

    private void ResetPathFinder()
    {
        if (_todoList != null) _todoList.ForEach(resetNode);
        if (_doneList != null) _doneList.ForEach(resetNode);

        _todoList = new List<HexNode>();
        _doneList = new List<HexNode>();
        _done = false;
        _path = null;
        _currentNode = null;

        //setup for next path
        if (_startNode != null)
        {
            _todoList.Add(_startNode);
            _startNode.CostCurrent = 0;
            _startNode.CostEstimate = 0;
            _startNode.CostCombined = 0;
        }

        if (_endNode != null)
        {
        }
    }

    private void resetNode(HexNode node)
    {
        if (node.Parent != null)
        {
            node.Parent = null;
        }
    }
}
