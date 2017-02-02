using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.FSMPlayer
{
    public class PlayerStateWalking : PlayerStateBase
    {
        private float       _movementSpeed           = 0.5f;
        private float       _rotationTime            = 1.0f;
        private float       _rotationAccumulator     = 0.0f;
        private HexNode     _targetNode;
        private Quaternion  _targetRotation;

        public PlayerStateWalking(PlayerAgent agent) : base(agent)
        {
        }

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
            if (Agent.WalkPath.Count > 1)
            {
                bool endAutoWalk = false;
                BreadthFirst enemyScan = new BreadthFirst();
                enemyScan.Search(Agent.CurrentNode, Agent.HighlighterRadius + 1);
                if (enemyScan.Done && enemyScan.Nodes != null)
                {
                    foreach (var node in enemyScan.Nodes)
                    {
                        if (node.HasEnemy)
                        {
                            endAutoWalk = true;
                            break;
                        }
                    }
                }
                if (endAutoWalk == false)
                {
                    Agent.WalkPath.RemoveAt(0);
                }
                else
                {
                    Agent.WalkPath = null;
                }

            }
            else
            {
                Agent.WalkPath = null;
            }
        }
    }
}