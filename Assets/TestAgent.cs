using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
	// Use this for initialization
	void Start ()
	{
	    _manager = Manager.GetComponent<HexNodesManager>();
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
	    }
	    if (startNode != null && endNode != null)
	    {
	        Vector3 sP = startNode.GetPosition();
	        Debug.DrawLine(sP, new Vector3(sP.x, sP.y + 20, sP.z), Color.blue);
	        Vector3 eP = endNode.GetPosition();
	        Debug.DrawLine(eP, new Vector3(eP.x, eP.y + 20, eP.z), Color.green);
	    }
	    if (Input.GetKeyDown(KeyCode.U))
	    {
	        _path = AStar.Search(startNode, endNode);
	        Debug.Log(_path.Count);
	    }
	    if (_path != null)
	    {
	        foreach (var node in _path)
	        {
	            Debug.DrawLine(node.GetPosition(), node.Parent.GetPosition(), Color.yellow);
	        }
	    }
	}
}
