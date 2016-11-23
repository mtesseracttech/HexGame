using System.Collections.Generic;
using Assets.Scripts.AI.GameStep.FSM.Agents;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class PlayerStateStepMovement : PlayerStateBase
    {
        private HexNode _currentNode;
        private HexNode _targetNode;
        private Vector3 _path;
        private float   _speed = 0.5f;
        private Vector3 _speedStep;

        public PlayerStateStepMovement(PlayerAgent agent) : base(agent)
        {
            Agent = agent;
        }

        public override void Update()
        {
            if (Vector3.Distance(Agent.Position, _targetNode.Position) > _speed)
            {
                Agent.Position -= _speedStep;
            }
            else
            {
                Agent.Position = _targetNode.Position;
                Agent.SetState(typeof(PlayerStateIdle));
            }
        }

        public override void BeginState()
        {
            _currentNode = Agent.CurrentNode;
            _targetNode  = Agent.TargetNode;
            _path        = _currentNode.Position - _targetNode.Position;
            _speedStep   = _path.normalized * _speed;
        }

        public override void EndState()
        {
            _currentNode = null;
            _targetNode  = null;
            _path        = Vector3.zero;
            _speedStep   = Vector3.zero;
        }
    }
}