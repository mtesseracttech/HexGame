using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI.Pathfinding
{
    public class Pathfinder
    {
        private List<HexNode>     _todoList;
        private List<HexNode>     _doneList;
        private List<HexNode>     _path;
        private HexNode           _startNode;
        private HexNode           _endNode;
        private HexNode           _currentNode;
        private bool              _done;

        public void Search(HexNode start, HexNode end)
        {
            SetStartNode(start);
            SetEndNode(end);
            while (!_done)
            {
                Step();
            }
        }

        public void Search()
        {
            while (!_done)
            {
                Step();
            }
        }

        public bool Step()
        {
            //are we able to find a path??
            if (_done || _startNode == null || _endNode == null || _todoList.Count == 0)
            {
                _done = true;
                return true;
            }

            //we are not done, start and end are set and there is at least 1 item on the open list...

            //get a node from the open list
            _currentNode = _todoList[0];
            _todoList.RemoveAt(0);

            //and move that node to the closed list (one way or another, we are done with it...)
            _doneList.Add(_currentNode);

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
                    HexNode neighbor = _currentNode.Neighbors[i];

                    if (_doneList.IndexOf(neighbor) == -1 && _todoList.IndexOf(neighbor) == -1)
                    {
                        if (neighbor.CostCurrent < _currentNode.CostCurrent)
                        {
                            neighbor.Parent = _currentNode;
                        }

                        neighbor.CostCurrent = _currentNode.CostCurrent +
                                               Vector3.Distance(_currentNode.Position, neighbor.Position);

                        neighbor.CostEstimate = Vector3.Distance(neighbor.Position, _endNode.Position);

                        neighbor.CostCombined = neighbor.CostCurrent + neighbor.CostEstimate;

                        _todoList.Add(neighbor);
                    }
                }

                _todoList.Sort();
            }

            return _done;
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

        public bool Done
        {
            get
            {
                return _done;
            }
        }

        public List<HexNode> Path
        {
            get { return _done && _path != null ? _path : null; }
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
        }

        private void resetNode(HexNode node)
        {
            if (node.Parent != null)
            {
                node.Parent = null;
            }
        }

        public override string ToString()
        {
            return
            (
                "Pathfinder Status:" +                    "\n" +
                "TodoList Length: "  + _todoList.Count  + "\n" +
                "DoneList Length: "  + _doneList.Count  + "\n" +
                "Start Node Index: " + _startNode.Index + "\n" +
                "End Node Index: "   + _endNode.Index   + "\n" +
                "Path Length: "      + _path.Count      + "\n" +
                "Done?: "            + _done
            );
        }

        public void HighLightDoneList()
        {
            if (_doneList != null)
            {
                foreach (var doneNode in _doneList)
                {
                    Debug.DrawLine(doneNode.Position, doneNode.Position + Vector3.up*5 + Vector3.forward, Color.red);
                }
            }
        }

        public void HighLightTodoList()
        {
            if (_todoList != null)
            {
                foreach (var todoNode in _todoList)
                {
                    Debug.DrawLine(todoNode.Position, todoNode.Position + Vector3.up*5 + Vector3.back, Color.blue);
                }
            }
        }
    }
}