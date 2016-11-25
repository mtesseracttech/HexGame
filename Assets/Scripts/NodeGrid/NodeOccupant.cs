using UnityEngine;
using System.Collections;

public abstract class NodeOccupant : MonoBehaviour {

	public GameObject hexNodes;
	protected HexNodesManager _hexNodeManager;

	public int nodeIndex;

	public virtual void Start () {
		_hexNodeManager = hexNodes.GetComponent <HexNodesManager> ();
	}

	public abstract void SetOccupant ();
}