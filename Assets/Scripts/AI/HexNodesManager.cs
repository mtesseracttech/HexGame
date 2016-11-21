﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Assets.Scripts.AI;
using Assets.Scripts.Saving;
using UnityEngine.Networking.Types;

public class HexNodesManager : MonoBehaviour
{
    public bool DebugMode = false;

    private HexNode[] _nodes;
	
	// Update is called once per frame
	void Update ()
	{
	    if (DebugMode && _nodes != null)
	    {
	        foreach (var node in _nodes)
	        {
	            foreach (var neighbor in node.Neighbors)
	            {
	                Debug.DrawLine(node.GetPosition(), neighbor.GetPosition(), Color.red);
	            }
	        }
	    }
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
}