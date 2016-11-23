using Assets.Scripts.AI.GameStep.FSMEnemy;
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
            if (Done)
            {
                if (Manager.SetNextEnemy(Enemy))
                {
                    Debug.Log("Ending Current Enemy Phase, Setting next one!");
                    Manager.ChangePhase(typeof(TurnPhaseEnemySelection));
                }
                else
                {
                    Debug.Log("End of the step!");
                    Manager.ChangePhase(typeof(TurnPhaseIdle));
                }
            }
            else
            {
                Debug.Log("Enemy is executing actions");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Done = true;
                }
            }
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