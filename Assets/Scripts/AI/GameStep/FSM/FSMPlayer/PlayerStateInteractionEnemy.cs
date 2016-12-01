using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.FSMPlayer
{
    public class PlayerStateInteractionEnemy : PlayerStateBase
    {
        private HexNode    _enemyNode;
        private EnemyAgent _enemy;

        public PlayerStateInteractionEnemy(PlayerAgent agent) : base(agent){}

        public override void Update()
        {
            Debug.Log("Player Attacking " + _enemy.AgentName + "! Press SPACE to continue!");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Agent.SetState(typeof(PlayerStateIdle));
            }

        }

        public override void BeginState()
        {
            _enemyNode = Agent.InteractionTarget;
            _enemy     = Agent.InteractionTarget.Occupant as EnemyAgent;
        }

        public override void EndState()
        {
            Agent.InteractionTarget        = null;
            Agent.UpcomingInteractionState = null;
        }
    }
}