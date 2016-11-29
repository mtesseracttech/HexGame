using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.AI;
using Assets.Scripts.AI.Pathfinding;
using Assets.Scripts.Saving;

public class HexNodesManager : MonoBehaviour
{
    public bool DebugMode = false;
    public bool ShowAllOccupiedNodes = true;

    private Pathfinder _pathfinder;

    private HexNode[] _nodes;

    private void Start()
    {
        _pathfinder = new Pathfinder();
    }

    // Update is called once per frame
	void Update ()
	{
	    if (DebugMode && _nodes != null)
	    {
	        foreach (var node in _nodes)
	        {
	            foreach (var neighbor in node.Neighbors)
	            {
	                Debug.DrawLine(node.Position, neighbor.Position, Color.red);
	            }
	        }
	    }

	    if (ShowAllOccupiedNodes && _nodes != null)
	    {
	        foreach (var node in _nodes)
	        {
	            if (node.HasOccupant)
	            {
	                Debug.DrawLine(node.Position, node.Position + Vector3.up*10 + Vector3.left, Color.white);
	            }
	        }
	    }

	}

	public void SetBuildingTiles () {
		HexNode i = GetHexNode (301);
		Debug.Log (GetHexNode (300).Occupant);

		Debug.Log ("Multi building: " + GetHexNode (10).Occupant + " " + GetHexNode (11).Occupant + " " + GetHexNode (12).Occupant + " " + GetHexNode (13).Occupant + " " + GetHexNode (14).Occupant + " NOTHING " + GetHexNode (15).Occupant);
	}

    public Pathfinder Pathfinder
    {
        get { return _pathfinder; }
    }

    public void SetNodesFromInfoContainer(HexCellInfoContainer[] infoContainers, bool cullBadConnections = false)
    {
        Debug.Log(infoContainers);

        HexNode[] nodes = new HexNode[infoContainers.Length];
        for (int i = 0; i < infoContainers.Length; i++)
        {
            nodes[i] = new HexNode(infoContainers[i]);
        }

        for (int i = 0; i < nodes.Length; i++)
        {
            List<HexNode> nodeNeighbors = new List<HexNode>();

            int[] nIndexes = infoContainers[i].NeighborIndexes;

            foreach (var nIndex in nIndexes)
            {
                nodeNeighbors.Add(nodes[nIndex]);
            }
            nodes[i].Neighbors = nodeNeighbors.ToArray();
        }

        if (cullBadConnections)
        {
            _nodes = CullBadConnections(nodes);
        }
        else _nodes = nodes;
    }

    public HexNode[] CullBadConnections(HexNode[] nodes)
    {
        for (int index = 0; index < nodes.Length; index++)
        {
            var node = nodes[index];
            List<HexNode> newNeighbors = new List<HexNode>();
            foreach (var neighbor in node.Neighbors)
            {
                if(IsConnectionValid(node, neighbor))
                {
                    newNeighbors.Add(neighbor);
                }
            }
            node.Neighbors = newNeighbors.ToArray();
        }
        return nodes;
    }

    public bool IsConnectionValid(HexNode current, HexNode other)
    {
        return Mathf.Abs(current.Elevation - other.Elevation) <= 1 &&
               !other.IsUnderWater && !current.IsUnderWater &&
               !other.HasRiver && !current.HasRiver;
    }

    public HexNode GetHexNode(int i)
    {
        if(_nodes != null && i < _nodes.Length) return _nodes[i];
        return null;
    }
}
