using UnityEngine;
using System.Collections;
using Assets.Scripts.AI.GameStep.FSM.Agents;

public class PlayerBase : MonoBehaviour
{
    private PlayerAgent _agent;


	// Use this for initialization
	void Start ()
	{
	    _agent = new PlayerAgent(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _agent.UpdateState();
	}
}
