using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.FSMEnemy
{
    public class EnemyStateStepMovement : EnemyStateBase
    {
        private HexNode _currentNode;
        private HexNode _targetNode;
        private Vector3 _path;
        private float   _speed         = 0.5f;
        private float   _slerpSpeed    = 0.05f;
        private Vector3 _speedStep;

        public EnemyStateStepMovement(EnemyAgent agent) : base(agent)
        {
            Agent = agent;
        }

        public override void Update()
        {
            Debug.Log("Enemy is walking!!!");
            if (Vector3.Distance(Agent.Position, _targetNode.Position) > _speed)
            {
                Agent.Position -= _speedStep;
            }
            else
            {
                Agent.CurrentNode = _targetNode;
                Agent.SetState(typeof(EnemyStateIdle));
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