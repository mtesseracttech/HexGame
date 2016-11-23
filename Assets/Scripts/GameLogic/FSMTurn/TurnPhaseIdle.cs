using Assets.Scripts.AI;
using Assets.Scripts.AI.GameStep.FSMEnemy;
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
            if (!Done)
            {
                Debug.Log("Idling...");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Done = true;
                }
            }
            else
            {
                Debug.Log("Step Starts!");
                Manager.ChangePhase(typeof(TurnPhasePlayerSelection));
            }

        }

        public override void Start()
        {
            Done = false;
            Manager.SetPlayerStates(typeof(PlayerStateIdle));
            Manager.SetEnemyStates (typeof(EnemyStateIdle) );
        }

        public override void End()
        {

        }
    }
}