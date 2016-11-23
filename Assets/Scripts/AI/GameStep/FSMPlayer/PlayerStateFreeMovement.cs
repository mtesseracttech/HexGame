namespace Assets.Scripts.AI
{
    public class PlayerStateFreeMovement : PlayerStateBase
    {
        private HexNode _targetNode;
        private HexNode _currentNode;
        private HexNode _totalPath;

        public PlayerStateFreeMovement(PlayerAgent agent) : base(agent)
        {
        }

        public override void Update()
        {
        }

        public override void BeginState()
        {
        }

        public override void EndState()
        {
        }
    }
}