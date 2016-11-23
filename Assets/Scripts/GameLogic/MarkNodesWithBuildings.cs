//using UnityEngine;
//using System.Collections;
//using Assets.Scripts.AI;
//using System.Collections.Generic;
//
//public class MarkNodesWithBuildings : MonoBehaviour {
//
//	[SerializeField]
//	GameObject NodeManager;
//
//	private List <HexNode> nodesWithBuildings = new List<HexNode> ();
//
//	private HexNodesManager _nodeManager;
//
//	void Start () {
//		_nodeManager = NodeManager.GetComponent <HexNodesManager> ();
//		HexNode node = _nodeManager.GetHexNode (301);
//		node.HasBuilding = true;
//	}
//
//	void Update () {
//		
//	}
//}