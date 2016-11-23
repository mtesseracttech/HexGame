namespace Assets.Scripts.AI
{
    public class PlayerStateFreeMovement : PlayerStateBase
    {
        private Node targetNode;
        private Node currentNode;
        private Node totalPath;

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