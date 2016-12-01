using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.FSMEnemy
{
    public class EnemyStateInteractionPlayer : EnemyStateBase
    {
        private HexNode     _playerNode;
        private PlayerAgent _player;

        public EnemyStateInteractionPlayer(EnemyAgent agent) : base(agent) {}

        public override void Update()
        {
            Debug.Log(Agent.AgentName + " is attacking " + _player.name);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Agent.SetState(typeof(EnemyStateIdle));
            }
        }

        public override void BeginState()
        {
            _playerNode = Agent.InteractionTarget;
            _player     = Agent.InteractionTarget.Occupant as PlayerAgent;
        }

        public override void EndState()
        {
            Agent.InteractionTarget        = null;
            Agent.UpcomingInteractionState = null;
        }
    }
}