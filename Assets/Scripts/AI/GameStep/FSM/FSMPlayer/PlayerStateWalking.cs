using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.FSMPlayer
{
    public class PlayerStateWalking : PlayerStateBase
    {
        private float       _movementSpeed           = 0.5f;
        private float       _rotationTime            = 0.5f;
        private float       _rotationAccumulator     = 0.0f;
        private HexNode     _targetNode;
        private Quaternion  _targetRotation;

        public PlayerStateWalking(PlayerAgent agent) : base(agent)
        {
        }

        public override void Update()
        {
            DebugHelpers.DebugList(Agent.WalkPath, "Walk Path: ");

            if (_rotationAccumulator < _rotationTime)
            {
                _rotationAccumulator += Time.deltaTime;
            }
            else
            {
                _rotationAccumulator = _rotationTime;
            }

            float rotationFactor = _rotationAccumulator / _rotationTime;

            Debug.Log("rotationAccumulator: " + _rotationAccumulator + " rotationFactor: " + rotationFactor);

            if (Vector3.Distance(Agent.Position, _targetNode.Position) > _movementSpeed)
            {
                Agent.Rotation = Quaternion.Slerp(Agent.Rotation, _targetRotation, rotationFactor);
                Agent.Position -= (Agent.Position - _targetNode.Position).normalized * _movementSpeed;
                Debug.Log("Moving!");
            }
            else
            {
                Debug.Log("Done Moving!");
                Agent.CurrentNode = _targetNode;
                Agent.SetState(typeof(PlayerStateIdle));
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