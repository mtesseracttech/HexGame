using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.GameStep.FSM.FSMEnemy;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhaseEnemyAction : TurnPhaseEnemyBase
    {
        public TurnPhaseEnemyAction(TurnManager manager, EnemyAgent enemy) : base(manager, enemy){}

        public override void Update()
        {
            if (Enemy.IsIdling())
            {
                if (Enemy.WalkPath != null)
                {
                    Enemy.SetState(typeof(EnemyStateWalking));
                }
                else if (Enemy.UpcomingInteractionState != null)
                {
                    Enemy.SetState(typeof(EnemyStateInteractionPlayer));
                }
                else if (Enemy.WalkPath == null && Enemy.UpcomingInteractionState == null)
                {
                    Debug.Log("Done with" + Enemy.AgentName +", switching to next phase!");
                    Manager.ChangePhase(typeof(TurnPhaseEnemyChange));
                }
            }
        }

        public override void Start()
        {
            Enemy = Manager.GetCurrentEnemy();
            Enemy.SetState(typeof(EnemyStateIdle));
        }

        public override void End()
        {

        }
    }
}