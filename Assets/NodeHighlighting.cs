using UnityEngine;
using System.Collections;

public class NodeHighlighting : MonoBehaviour {

	public GameObject NodeManager;
	private HexNodesManager _nodeManager;


	// Use this for initialization
	void Start () 
	{
		_nodeManager = NodeManager.GetComponent<HexNodesManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.L))
		{
			_nodeManager.GetHexNode (50);
		}
	}
}
