
//WORK IN PROGRESS, NO TOUCHIES!

using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI.Pathfinding
{
    public class PathfinderV2
    {
        private List<HexNode>     _openList;
        private List<HexNode>     _closedList;
        private List<HexNode>     _path;
        private HexNode           _startNode;
        private HexNode           _endNode;
        private bool              _done;


        public void Search(HexNode start, HexNode end)
        {
            if (_startNode != null) ResetNode(_startNode);
            if (_endNode   != null) ResetNode(_endNode  );

            _startNode = start;
            _endNode   = end;

            ResetPathfinder();

            Find();
        }

        private void Find()
        {
            _startNode.CostCurrent  = 0;
            _startNode.CostEstimate = Vector3.Distance(_startNode.Position, _endNode.Position);
            _startNode.CostCombined = _startNode.CostCurrent + _startNode.CostEstimate;

            _openList.Add(_startNode);

            while (_openList.Count > 0)
            {
                _openList.Sort();

                HexNode current = _openList[0];

                if (current == _endNode)
                {
                    ConstructPath(_endNode);
                    _done = true;
                    return;
                }

                _openList.Remove(current);
                _closedList.Add(current);

                foreach (var neighbor in current.Neighbors)
                {

                    neighbor.CostCurrent  = current.CostCurrent + Vector3.Distance(current.Position, neighbor.Position);

                    if (!_closedList.Contains(neighbor))
                    {
                        neighbor.CostEstimate = Vector3.Distance(neighbor.Position, _endNode.Position);
                        neighbor.CostCombined = neighbor.CostCurrent + neighbor.CostEstimate;

                        if (!_openList.Contains(neighbor))
                        {
                            _openList.Add(neighbor);
                        }
                        else
                        {
                            HexNode openNeighbor = _openList[_openList.IndexOf(neighbor)];

                            if (neighbor.CostCurrent < openNeighbor.CostCurrent)
                            {
                                openNeighbor.CostCurrent = neighbor.CostCurrent;
                                openNeighbor.Parent      = neighbor.Parent;
                            }
                        }
                    }
                }
            }
            _done = true;
        }

        private void ConstructPath(HexNode end)
        {
            _path = new List<HexNode>();
            HexNode node = end;
            while (node != null)
            {
                _path.Add(node);
                node = node.Parent;
            }
            _path.Reverse();
        }

        public void ResetPathfinder()
        {
            if(_openList   != null) _openList.ForEach(ResetNode);
            if(_closedList != null) _closedList.ForEach(ResetNode);

            _openList    = new List<HexNode>();
            _closedList  = new List<HexNode>();
            _done        = false;
            _path        = null;
        }

        private void ResetNode(HexNode node)
        {
            node.CostCurrent  = 0;
            node.CostEstimate = 0;
            node.CostCombined = 0;

            if (node.Parent != null) node.Parent = null;
        }

        public List<HexNode> Path
        {
            get { return _path; }
        }

        public bool IsDone
        {
            get { return _done; }
        }
    }
}


//g is current
//h is estimate
//f is combined

/*
function A*(start, goal)
  open_list = set containing start
  closed_list = empty set
  start.g = 0
  start.f = start.g + heuristic(start, goal)
  while open_list is not empty
    current = open_list element with lowest f cost
    if current = goal
      return construct_path(goal) // path found
    remove current from open_list
    add current to closed_list
    for each neighbor in neighbors(current)
      if neighbor not in closed_list
        neighbor.f = neighbor.g + heuristic(neighbor, goal)
        if neighbor is not in open_list
          add neighbor to open_list
        else
          openneighbor = neighbor in open_list
          if neighbor.g < openneighbor.g
            openneighbor.g = neighbor.g
            openneighbor.parent = neighbor.parent
  return false // no path exists

function neighbors(node)
  neighbors = set of valid neighbors to node // check for obstacles here
  for each neighbor in neighbors
    if neighbor is diagonal
      neighbor.g = node.g + diagonal_cost // eg. 1.414 (pythagoras)
    else
      neighbor.g = node.g + normal_cost // eg. 1
    neighbor.parent = node
  return neighbors

function construct_path(node)
  path = set containing node
  while node.parent exists
    node = node.parent
    add node to path
  return path
*/