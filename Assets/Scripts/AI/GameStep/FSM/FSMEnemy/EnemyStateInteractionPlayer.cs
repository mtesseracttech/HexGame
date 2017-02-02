using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.FSMEnemy
{
    public class EnemyStateInteractionPlayer : EnemyStateBase
    {
        private PlayerAgent _player;
        private float       _rotationTime            = 1.0f;
        private float       _rotationAccumulator     = 0.0f;
        private Quaternion  _targetRotation;
        private Quaternion  _playerTargetRotation;

        public EnemyStateInteractionPlayer(EnemyAgent agent) : base(agent) {}

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

            Agent.Rotation   = Quaternion.Slerp(Agent.Rotation  , _targetRotation      , rotationFactor * (Time.deltaTime * 60));
            _player.Rotation = Quaternion.Slerp(_player.Rotation, _playerTargetRotation, rotationFactor * (Time.deltaTime * 60));

            Debug.Log(Agent.AgentName + " is attacking " + _player.name);

            Agent.CombatUi.SetActive(true);
            if (Agent.CoinFlip.AttackEnds)
            {
                Agent.SetState(typeof(EnemyStateIdle));
                Agent.CoinFlip.AttackEnds = false;
                Agent.CoinFlip.AttackButton = false;
            }
        }

        public override void BeginState()
        {
            Agent.CoinFlip._enemyImage.sprite = Agent.EnemyBattleImage;
            _player                           = Agent.InteractionTarget.Occupant as PlayerAgent;
            if (_player != null)
            {
                _targetRotation                   = Quaternion.LookRotation(_player.Position - Agent.Position);
                _playerTargetRotation             = Quaternion.LookRotation(Agent.Position - _player.Position);
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