using UnityEngine;
using System.Collections;

public class EnemyOccupant : NodeOccupant {

	public override void Start () {
		_hexNodeManager = hexNodes.GetComponent <HexNodesManager> ();
		SetOccupant ();
	}

	public override void SetOccupant () {
		_hexNodeManager.GetHexNode (nodeIndex).Occupant = this;
	}

	public void Relocate (int currentNode, int targetNode) {
		_hexNodeManager.GetHexNode (currentNode).Occupant = null;
		_hexNodeManager.GetHexNode (targetNode).Occupant = this;
	}
}
