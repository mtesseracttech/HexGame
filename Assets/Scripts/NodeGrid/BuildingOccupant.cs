using UnityEngine;
using System.Collections;

public class BuildingOccupant : NodeOccupant {

	public override void Start () {		
		_hexNodeManager = hexNodes.GetComponent <HexNodesManager> ();
		SetOccupant ();
	}

	public override void SetOccupant () {
		_hexNodeManager.GetHexNode (nodeIndex).Occupant = this;
	}
}
