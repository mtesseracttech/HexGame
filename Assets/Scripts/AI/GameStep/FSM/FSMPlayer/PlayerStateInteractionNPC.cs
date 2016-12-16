using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.NodeGrid.Occupants.Specifics;

namespace Assets.Scripts.AI.GameStep.FSM.FSMPlayer
{
    public class PlayerStateInteractionNPC : PlayerStateBase
    {
        private bool _interactionDone;
        private NPCOccupant NPC;

        public PlayerStateInteractionNPC(PlayerAgent agent) : base(agent)
        {
        }

        public override void Update()
        {
            if (!_interactionDone)
            {

            }
            else
            {
                Agent.SetState(typeof(PlayerStateIdle));
            }
        }

        public override void BeginState()
        {
            _interactionDone = false;
            NPC = Agent.InteractionTarget.Occupant as NPCOccupant;
        }

        public override void EndState()
        {
            Agent.InteractionTarget        = null;
            Agent.UpcomingInteractionState = null;
        }
    }
}