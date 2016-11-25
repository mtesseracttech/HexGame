using UnityEngine;
using System.Collections;

public class MultiBuildingOccupant : NodeOccupant {

	public int[] nodeIndexes;

	public override void Start () {		
		_hexNodeManager = hexNodes.GetComponent <HexNodesManager> ();
		SetOccupant ();
	}

	public override void SetOccupant () {
		for (int i = 0; i < nodeIndexes.Length; i++) {
			_hexNodeManager.GetHexNode (nodeIndexes[i]).Occupant = this;
		}
	}
}
