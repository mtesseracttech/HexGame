using UnityEngine;
using System.Collections;

public class StaticMultiNodeOccupant : NodeOccupant {

	public int[] NodeIndexes;

	public override void Start () {		
		HexNodeManager = HexNodes.GetComponent <HexNodesManager> ();
		SetOccupant ();
	}

	public override void SetOccupant () {
		for (int i = 0; i < NodeIndexes.Length; i++) {
			HexNodeManager.GetHexNode (NodeIndexes[i]).Occupant = this;
		}
	}
}
