using UnityEngine;
using System.Collections;

public class StaticNodeOccupant : NodeOccupant {

	public override void Start () {		
		HexNodeManager = HexNodes.GetComponent <HexNodesManager> ();
		SetOccupant ();
	}

	public override void SetOccupant () {
		HexNodeManager.GetHexNode (NodeIndex).Occupant = this;
	}
}
