using UnityEngine;
using System.Collections;

public class MobileNodeOccupant : NodeOccupant {

	public override void Start () {
		HexNodeManager = HexNodes.GetComponent <HexNodesManager> ();
		SetOccupant ();
	}

	public override void SetOccupant () {
		HexNodeManager.GetHexNode (NodeIndex).Occupant = this;
	}

	public void Relocate (int currentNode, int targetNode) {
		HexNodeManager.GetHexNode (currentNode).Occupant = null;
		HexNodeManager.GetHexNode (targetNode).Occupant = this;
	}
}
