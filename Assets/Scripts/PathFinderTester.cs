using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

public class PathFinderTester : MonoBehaviour
{

    public GameObject HexnodeManager;
    private HexNodesManager hnm;

    public int Index1 = 100;
    public int Index2 = 200;

	// Use this for initialization
	void Start ()
	{
	    hnm = HexnodeManager.GetComponent<HexNodesManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	        Debug.Log("Finding path!");

	      //  Pathfinder pf = new Pathfinder();
	       // pf.Search(hnm);
	    }
	}
}
