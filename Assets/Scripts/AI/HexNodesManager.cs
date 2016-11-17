using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Assets.Scripts.Saving;
using UnityEngine.Networking.Types;

public class HexNodesManager : MonoBehaviour
{
    public bool DebugMode = false;

    private HexCellNode[] _nodes;
	
	// Update is called once per frame
	void Update ()
	{
	    if (DebugMode && _nodes != null)
	    {
	        foreach (var node in _nodes)
	        {
	            foreach (var neighbor in node.NeighborIndexes)
	            {
	                Debug.DrawLine(node.Position, _nodes[neighbor].Position, Color.red);
	            }
	        }
	    }
	}

    public void SetNodes(HexCellNode[] nodes, bool cullBadConnections = false)
    {
        if (cullBadConnections) _nodes = CullBadConnections(nodes);
        else _nodes = nodes;
    }

    public HexCellNode[] CullBadConnections(HexCellNode[] nodes)
    {
        for (int index = 0; index < nodes.Length; index++)
        {
            var node = nodes[index];
            List<int> newNeighborIndexes = new List<int>();
            foreach (var neighbor in node.NeighborIndexes)
            {
                if(ConnectionValid(node, nodes[neighbor]))
                {
                    newNeighborIndexes.Add(neighbor);
                }
            }
            node.NeighborIndexes = newNeighborIndexes.ToArray();
        }
        return nodes;
    }

    public bool ConnectionValid(HexCellNode current, HexCellNode other)
    {
        return Mathf.Abs(current.Elevation - other.Elevation) <= 1 &&
               !other.IsUnderWater && !current.IsUnderWater &&
               !other.HasRiver && !current.HasRiver;
    }
}
