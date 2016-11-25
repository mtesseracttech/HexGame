using UnityEngine;
using System.Collections;

public abstract class NodeOccupant : MonoBehaviour {

    public int NodeIndex;
    public GameObject HexNodes;
	protected HexNodesManager HexNodeManager;

	public virtual void Start ()
	{
		HexNodeManager = HexNodes.GetComponent <HexNodesManager> ();
	}

	public abstract void SetOccupant ();
}