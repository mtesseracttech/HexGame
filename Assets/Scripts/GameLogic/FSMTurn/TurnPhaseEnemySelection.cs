using Assets.Scripts.AI;
using Assets.Scripts.AI.GameStep.FSM.Agents;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhaseEnemySelection : TurnPhaseEnemyBase
    {
        private PlayerAgent _player;

        public TurnPhaseEnemySelection(TurnManager manager, EnemyAgent enemy) : base(manager, enemy)
        {
            _player = Manager.GetPlayerAgent();
        }

        public override void Update()
        {
            //Not Needed here, enemy can make selection and nextphase in 1 go in the start block
        }

        public override void Start()
        {
            Enemy = Manager.RefreshCurrentEnemy();
            Debug.Log("Enemy Making Selection!");

            HexNode playerNode = _player.CurrentNode;
            HexNode ownNode    = Enemy.CurrentNode;













            Manager.ChangePhase(typeof(TurnPhaseEnemyAction));
        }

        public override void End()
        {
            //Not Needed
        }
    }
}