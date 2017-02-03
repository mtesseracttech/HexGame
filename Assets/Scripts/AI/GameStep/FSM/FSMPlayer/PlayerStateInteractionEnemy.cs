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
        private Quaternion _targetRotation;
        private Quaternion _enemyTargetRotation;

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

            _enemy.CoinFlip.EnemyStats = _enemy._EnemyStats;

            Agent.Rotation  = Quaternion.Slerp( Agent.Rotation, _targetRotation     , rotationFactor);
            _enemy.Rotation = Quaternion.Slerp(_enemy.Rotation, _enemyTargetRotation, rotationFactor);

            Debug.Log("Player Attacking " + _enemy.AgentName + "! Press SPACE to continue!");

            Agent.CombatUi.SetActive(true);
            if (Agent.CoinFlip.AttackEnds)
            {
                Agent.SetState(typeof(PlayerStateIdle));
                Agent.CoinFlip.AttackEnds = false;
                Agent.CoinFlip.AttackButton = false;
            }

        }

        public override void BeginState()
        {

            _enemyNode           = Agent.InteractionTarget;
            _enemy               = Agent.InteractionTarget.Occupant as EnemyAgent;
            _targetRotation      = Quaternion.LookRotation(_enemyNode.Position - Agent.Position);
            _enemyTargetRotation = Quaternion.LookRotation(Agent.Position - _enemyNode.Position);
            _rotationAccumulator = 0;

            if (_enemy != null)
            {
                _enemy.CoinFlip._enemyImage.sprite = _enemy.EnemyBattleImage;
            }
        }

        public override void EndState()
        {
            Agent.CombatUi.SetActive(false);
            Agent.InteractionTarget        = null;
            Agent.UpcomingInteractionState = null;
            Agent.CoinFlip.AttackEnds      = false;
            Agent.CoinFlip.AttackButton    = false;
        }
    }
}