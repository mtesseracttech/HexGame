using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AI;
using Assets.Scripts.AI.GameStep.FSMEnemy;

public class TurnManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Grid;

    private PlayerActor _player;
    private List<EnemyActor> _enemies;


	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

    private void PlayerChoicePhase()
    {

    }

    private void PlayerActionPhase()
    {

    }

    private void EnemySelector()
    {
        foreach (var enemy in _enemies)
        {
            EnemyChoicePhase(enemy);
            EnemyActionPhase(enemy);
        }
    }

    private void EnemyChoicePhase(EnemyActor enemy)
    {
    }

    private void EnemyActionPhase(EnemyActor enemy)
    {
    }
}
