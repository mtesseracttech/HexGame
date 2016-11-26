using Assets.Scripts.AI;
using Assets.Scripts.AI.GameStep.FSM.FSMEnemy;
using Assets.Scripts.AI.GameStep.FSM.FSMPlayer;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhaseIdle : TurnPhaseBase
    {
        public TurnPhaseIdle(TurnManager manager) : base(manager)
        {
        }

        public override void Update()
        {
            Debug.Log("Step Starts!");
            Manager.ChangePhase(typeof(TurnPhasePlayerSelection));
        }

        public override void Start()
        {
            Done = false;
            Manager.RemoveDeadEnemies();
            Manager.SetPlayerStates(typeof(PlayerStateIdle));
            Manager.SetEnemyStates (typeof(EnemyStateIdle) );
        }

        public override void End()
        {
            //Manager.EnemyRadar!!!

            Manager.SetFirstEnemy();//Run after enemyradar, so every enemy gets picked up for this.
        }
    }
}