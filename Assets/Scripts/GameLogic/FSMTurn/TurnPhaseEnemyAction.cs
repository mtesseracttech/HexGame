using Assets.Scripts.AI.GameStep.FSM.Agents;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhaseEnemyAction : TurnPhaseEnemyBase
    {
        public TurnPhaseEnemyAction(TurnManager manager, EnemyAgent enemy) : base(manager, enemy)
        {
        }

        public override void Update()
        {
            /*
            if (Done)
            {
                if (Manager.HasNextEnemy())
                {
                    Manager.SetNextEnemy();
                    Debug.Log("Ending Current Enemy Phase, Changed to next one!");
                    Manager.ChangePhase(typeof(TurnPhaseEnemySelection));
                }
                else
                {
                    Debug.Log("No more enemies to do. End of the step!");
                    Manager.ChangePhase(typeof(TurnPhaseIdle));
                }
            }
            else
            {
                Debug.Log("Enemy is executing actions");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Done = true;
                    Debug.Log("Continuing to next");
                }
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                Manager.ChangePhase(typeof(TurnPhaseIdle));
            }
            */
        }

        public override void Start()
        {
            Done = false;
        }

        public override void End()
        {
        }
    }
}