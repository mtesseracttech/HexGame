using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.AI;

public class TestAgent : MonoBehaviour
{
    public GameObject Manager;
    public int StartNodeIndex;
    public int EndNodeIndex;

    private HexNodesManager _manager;
    private HexNode startNode;
    private HexNode endNode;
    private List<HexNode> _path;
    private AStar _pathfinder;
	// Use this for initialization
	void Start ()
	{
	    _manager = Manager.GetComponent<HexNodesManager>();
	    _pathfinder = new AStar();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    startNode = _manager.GetHexNode(StartNodeIndex);
        endNode = _manager.GetHexNode(EndNodeIndex);
	    if (Input.GetKeyDown(KeyCode.I))
	    {

	        if (startNode != null && endNode != null)
	        {
	            Debug.Log("Successfully loaded the nodes into the testagent!");
	        }
	        else
	        {
	            Debug.Log("Could not properly load the nodes!");
	        }
	    }
	    if (startNode != null && endNode != null)
	    {
            Debug.Log(startNode);
            Debug.Log(endNode);
	        Vector3 sP = startNode.Position;
	        Debug.DrawLine(sP, new Vector3(sP.x, sP.y + 20, sP.z), Color.blue);
	        Vector3 eP = endNode.Position;
	        Debug.DrawLine(eP, new Vector3(eP.x, eP.y + 20, eP.z), Color.green);
	    }
	    if (Input.GetKeyDown(KeyCode.U))
	    {
	        if (startNode != null && endNode != null)
	        {
	            _pathfinder.Search(startNode, endNode);
	            Debug.Log("Starting to search");
	            if (_pathfinder.Done)
	            {
	                _path = _pathfinder.Path;
	                if (_path != null)
	                {
	                    Debug.Log("Path found!\n" + "Path length : " + _path.Count);
	                }
	                else
	                {
	                    Debug.Log("No solution was found!");
	                }
	            }
	        }
	        else
	        {
	            Debug.Log("the start/end node was not set yet!");
	        }
	    }

	    if (_path != null)
	    {
	        DebugPath();
	    }


	}

    private void DebugPath()
    {
        for (int i = 0; i < _path.Count -1 ; i++)
        {
            try
            {
                Debug.DrawLine(_path[i].Position, _path[i+1].Position);
            }
            catch (Exception)
            {
                Debug.Log("Shit goes wrong at " + i);
            }

        }
    }
}
