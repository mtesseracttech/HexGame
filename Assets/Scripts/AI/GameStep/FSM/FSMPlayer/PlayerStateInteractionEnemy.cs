using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.FSMPlayer
{
    public class PlayerStateInteractionEnemy : PlayerStateBase
    {
        private HexNode    _enemyNode;
        private EnemyAgent _enemy;
        private float      _rotationTime            = 0.5f;
        private float      _rotationAccumulator     = 0.0f;
        private Quaternion  _targetRotation;

        public PlayerStateInteractionEnemy(PlayerAgent agent) : base(agent){}

        public override void Update()
        {
            if (_rotationAccumulator < _rotationTime)
            {
                _rotationAccumulator += Time.deltaTime;
            }
            else
            {
                _rotationAccumulator = _rotationTime;
            }

            float rotationFactor = _rotationAccumulator / _rotationTime;
            Agent.Rotation = Quaternion.Slerp(Agent.Rotation, _targetRotation, rotationFactor);

            Agent.CombatUi.SetActive(true);

            Debug.Log("Player Attacking " + _enemy.AgentName + "! Press SPACE to continue!");
            
            if (Agent.CoinFlip.AttackEnds)
            {
               
                Agent.SetState(typeof(PlayerStateIdle));
            }

        }

        public override void BeginState()
        {
            _enemyNode = Agent.InteractionTarget;
            _enemy     = Agent.InteractionTarget.Occupant as EnemyAgent;
            _targetRotation      = Quaternion.LookRotation(_enemyNode.Position - Agent.Position);
            _rotationAccumulator = 0;
        }

        public override void EndState()
        {
            Agent.CombatUi.SetActive(false);
            Agent.InteractionTarget        = null;
            Agent.UpcomingInteractionState = null;
        }
    }
}