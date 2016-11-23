
using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.GameStep.FSMEnemy;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhaseEnemySelection : TurnPhaseEnemyBase
    {
        public TurnPhaseEnemySelection(TurnManager manager, EnemyAgent enemy) : base(manager, enemy)
        {
        }

        public override void Update()
        {
            //Not Needed here, enemy can make selection and nextphase in 1 go in the start block
        }

        public override void Start()
        {
            Enemy = Manager.RefreshCurrentEnemy();
            Debug.Log("Enemy Making Selection!");
            Manager.ChangePhase(typeof(TurnPhaseEnemyAction));
        }

        public override void End()
        {
            //Not Needed
        }
    }
}