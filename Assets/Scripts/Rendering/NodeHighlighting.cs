﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.AI;
using Assets.Scripts.AI.GameStep.FSM.Agents;


public class NodeHighlighting : MonoBehaviour
{
    public int CurrentNodeIndex;
    private HexNode _currentNode;
	public GameObject NodeManager;
	private HexNodesManager _nodeManager;
	private BreadthFirst _pathfinder;
    public PlayerAgent Player;

	void Start ()
	{
		_nodeManager = NodeManager.GetComponent<HexNodesManager>();
		_pathfinder = GetComponent <BreadthFirst> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
        UpdateCurrentPosition();
		if (Input.GetKeyDown (KeyCode.Z)) {
			_nodeManager.SetBuildingTiles ();
			_nodeManager.SetEnemyTiles ();
		}

		if(Input.GetKeyDown(KeyCode.L))
		{
			//_pathfinder.ClearHighlights ();
			//StartCoroutine (_pathfinder.Search (_nodeManager.GetHexNode (CurrentNodeIndex)));
            //_nodeManager.GetHexNode (CurrentNodeIndex);
            OnGridShow();
        }

		if (Input.GetKeyDown (KeyCode.H)) {
		//	_pathfinder.ClearHighlights ();
        ClearGrid();
		}

		if (Input.GetKeyDown (KeyCode.A)) {
		    CurrentNodeIndex += 1;
			Debug.Log (CurrentNodeIndex);
		}
	}

	public void SetCurrentPosition (int position) {
	    CurrentNodeIndex = position;
	}

    public void UpdateCurrentPosition()
    {
        CurrentNodeIndex = Player.CurrentNode.Index;
    }

    public void OnGridShow()
    {
        StartCoroutine(_pathfinder.Search(_nodeManager.GetHexNode(CurrentNodeIndex)));
    }

    public void ClearGrid()
    {
        _pathfinder.ClearHighlights();
    }
}
