using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.FSMEnemy
{
    public class EnemyStateWalking : EnemyStateBase
    {
        private float       _movementSpeed           = 0.5f;
        private float       _rotationTime            = 1.0f;
        private float       _rotationAccumulator     = 0.0f;
        private HexNode     _targetNode;
        private Quaternion  _targetRotation;

        public EnemyStateWalking(EnemyAgent agent) : base(agent) {}

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

            if (Vector3.Distance(Agent.Position, _targetNode.Position) > _movementSpeed * (Time.deltaTime * 60))
            {
                Agent.Rotation = Quaternion.Slerp(Agent.Rotation, _targetRotation, rotationFactor * (Time.deltaTime * 60));
                Agent.Position -= (Agent.Position - _targetNode.Position).normalized * _movementSpeed * (Time.deltaTime * 60);
            }
            else
            {
                Agent.CurrentNode = _targetNode;
                Agent.SetState(typeof(EnemyStateIdle));
            }
        }

        public override void BeginState()
        {
            _targetNode          = Agent.WalkPath[0];
            _targetRotation      = Quaternion.LookRotation(_targetNode.Position - Agent.Position);
            _rotationAccumulator = 0.0f;
        }

        public override void EndState()
        {
            Agent.WalkPath = null;
        }
    }
}