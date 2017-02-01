using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.NodeGrid.Occupants.Specifics;

namespace Assets.Scripts.AI.GameStep.FSM.FSMPlayer
{
    public class PlayerStateInteractionNPC : PlayerStateBase
    {
        private NPCOccupant NPC;

        public PlayerStateInteractionNPC(PlayerAgent agent) : base(agent)
        {
        }

        public override void Update()
        {
            NPC.Interact();
            if (!NPC.GetInteractionOver())
            {
                NPC._dialogue.enabled = true;
                NPC._dialogue.ShowDialogue = true;
            }
            else 
            {
                Agent.SetState(typeof(PlayerStateIdle));
            }
        }

        public override void BeginState()
        {
           NPC = Agent.InteractionTarget.Occupant as NPCOccupant;
        }

        public override void EndState()
        {
            Agent.InteractionTarget        = null;
            Agent.UpcomingInteractionState = null;
        }
    }
}