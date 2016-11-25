using UnityEngine;
using System.Collections;

public class PropsOccupant : NodeOccupant {

	public override void Start () {
		_hexNodeManager = hexNodes.GetComponent <HexNodesManager> ();
		SetOccupant ();
	}

	public override void SetOccupant () {
		_hexNodeManager.GetHexNode (nodeIndex).Occupant = this;
	}

	public void PickedUp () {
		_hexNodeManager.GetHexNode (nodeIndex).Occupant = null;
	}
}
