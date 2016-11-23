using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AI;
using Assets.Scripts.AI.GameStep.FSMEnemy;

public class TurnManagerPseudoCode : MonoBehaviour
{
    public GameObject Player;
    public GameObject Grid;

    private PlayerAgent _player;
    private List<EnemyAgent> _enemies;


	// Use this for initialization
	void Start ()
	{
        _enemies = new List<EnemyAgent>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}


    private void StartStep()
    {
        PlayerStep();
        EnemiesStep();
    }

    private void PlayerStep()
    {
        PlayerChoicePhase();
        PlayerActionPhase();
    }

    private void PlayerChoicePhase()
    {
        _player.SetState(typeof(PlayerStateIdle));
        bool playerChoicesMade = false;
        while (!playerChoicesMade)
        {
            Debug.Log("Player is making choices");
            playerChoicesMade = true; //TEMP FOR DEBUGGING, SHOULD ONLY BECOME TRUE WHEN CHOICE IS ACTUALLY MADE!
        }
    }

    private void PlayerActionPhase()
    {
        _player.SetState(typeof(PlayerStateStepMovement));
        bool moving = true;
        while (moving)
        {
            Debug.Log("Player is moving");
            moving = false; //TEMP FOR DEBUGGING, SHOULD ONLY BECOME TRUE WHEN MOVEMENT IS FINISHED!
        }
        _player.SetState(typeof(PlayerStateAttack));
        bool attacking = true;
        while (attacking)
        {
            Debug.Log("Player is attacking");
            attacking = false; //TEMP FOR DEBUGGING, SHOULD ONLY BECOME TRUE WHEN ATTACKING IS FINISHED!
        }
        _player.SetState(typeof(PlayerStateIdle));
    }

    private void EnemiesStep()
    {
        foreach (var enemy in _enemies)
        {
            EnemyStep(enemy);
        }
    }
    private void EnemyStep(EnemyAgent enemy)
    {
        EnemyChoicePhase(enemy);
        EnemyActionPhase(enemy);
    }

    private void EnemyChoicePhase(EnemyAgent enemy)
    {
        enemy.SetState(typeof(EnemyStateIdle));

    }


    private void EnemyActionPhase(EnemyAgent enemy)
    {

    }

    public void AddEnemy(EnemyAgent enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemy(EnemyAgent enemy)
    {
        if (_enemies.Contains(enemy))
        {
            _enemies.Remove(enemy);
        }
    }

}
