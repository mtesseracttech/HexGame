using UnityEngine;
using System.Collections;
using Assets.Scripts.AI;

public class NodeHighlighting : MonoBehaviour
{

    public int CurrentNodeIndex = 300;
    private HexNode _currentNode;
	public GameObject NodeManager;
	private HexNodesManager _nodeManager;
	private BreadthFirst _pathfinder;

	// Use this for initialization
	void Start ()
	{
		_nodeManager = NodeManager.GetComponent<HexNodesManager>();
		_pathfinder = GetComponent <BreadthFirst> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Z)) {
			_nodeManager.SetBuildingTiles ();
			_nodeManager.SetEnemyTiles ();
		}

		if(Input.GetKeyDown(KeyCode.L))
		{
			//_pathfinder.ClearHighlights ();
			StartCoroutine (_pathfinder.Search (_nodeManager.GetHexNode (CurrentNodeIndex)));
            //_nodeManager.GetHexNode (CurrentNodeIndex);
        }

		if (Input.GetKeyDown (KeyCode.H)) {
			_pathfinder.ClearHighlights ();
		}

		if (Input.GetKeyDown (KeyCode.A)) {
		    CurrentNodeIndex += 1;
			Debug.Log (CurrentNodeIndex);
		}
	}

	void SetCurrentPosition (int position) {
	    CurrentNodeIndex = position;
	}
}
