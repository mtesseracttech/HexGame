using System.Collections.Generic;
using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.Pathfinding;

namespace Assets.Scripts.AI
{
    public class PlayerStateFreeMovement : PlayerStateBase
    {
        private HexNode _targetNode;
        private HexNode _currentNode;
        private List<HexNode> _totalPath;

        public PlayerStateFreeMovement(PlayerAgent agent) : base(agent){}

        public override void BeginState()
        {
            _totalPath = Agent.GetPath();
        }

        public override void Update()
        {

        }

        public override void EndState()
        {

        }
    }
}