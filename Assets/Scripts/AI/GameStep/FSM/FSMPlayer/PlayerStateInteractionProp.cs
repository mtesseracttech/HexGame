using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.Pathfinding;

namespace Assets.Scripts.AI.GameStep.FSM.FSMPlayer
{
    public class PlayerStateInteractionProp : PlayerStateBase
    {
        private HexNode _itemNode;
        private Items _item;

        public PlayerStateInteractionProp(PlayerAgent agent) : base(agent)
        {
        }

        public override void Update()
        {

        }

        public override void BeginState()
        {
            _itemNode = Agent.CurrentNode;
            _item = Agent.InteractionTarget.Occupant as Items;


        }

        public override void EndState()
        {
            Agent.InteractionTarget        = null;
            Agent.UpcomingInteractionState = null;
        }
    }
}