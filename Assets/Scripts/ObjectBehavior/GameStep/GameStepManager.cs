using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStepManager : MonoBehaviour
{
    private Dictionary<Type, GameStepBase> _gameSteps;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public abstract class GameStepBase
{
    public GameStepBase(GameObject owner){}

    public abstract void Update();
}

public class GameStepPlayer : GameStepBase
{
    public override void Update()
    {

    }

    public GameStepPlayer(GameObject owner) : base(owner)
    {
    }
}

public class GameStepEnemy : GameStepBase
{
    public override void Update()
    {

    }

    public GameStepEnemy(GameObject owner) : base(owner)
    {
    }
}


