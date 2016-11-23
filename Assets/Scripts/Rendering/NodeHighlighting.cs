using UnityEngine;
using System.Collections;

public class NodeHighlighting : MonoBehaviour {

	public GameObject NodeManager;
	private HexNodesManager _nodeManager;
	private BreadthFirst _pathfinder;

	int currentPositionOfPlayer;

	// Use this for initialization
	void Start () 
	{
		currentPositionOfPlayer = 300;
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
			StartCoroutine (_pathfinder.Search (_nodeManager.GetHexNode (currentPositionOfPlayer)));
			//_nodeManager.GetHexNode (currentPositionOfPlayer);

		}

		if (Input.GetKeyDown (KeyCode.H)) {
			_pathfinder.ClearHighlights ();
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			currentPositionOfPlayer += 1;
			Debug.Log (currentPositionOfPlayer);
		}
	}

	void SetCurrentPosition (int position) {
		currentPositionOfPlayer = position;
	}
}
