using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.NodeGrid.Occupants.Specifics;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.FSMPlayer
{
    public class PlayerStateInteractionNPC : PlayerStateBase
    {
        private NPCOccupant NPC;
        private float       _rotationTime            = 1.0f;
        private float       _rotationAccumulator     = 0.0f;
        private Quaternion  _targetRotation;
        private Quaternion  _npcTargetRotation;

        public PlayerStateInteractionNPC(PlayerAgent agent) : base(agent)
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

            Agent.Rotation         = Quaternion.Slerp(Agent.Rotation        , _targetRotation   , rotationFactor * (Time.deltaTime * 60));
            NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, _npcTargetRotation, rotationFactor * (Time.deltaTime * 60));

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
            if (NPC != null)
            {
                _targetRotation      = Quaternion.LookRotation(NPC.GetCurrentNode().Position - Agent.Position);
                _npcTargetRotation   = Quaternion.LookRotation(Agent.Position - NPC.GetCurrentNode().Position);
            }
        }

        public override void EndState()
        {
            Agent.InteractionTarget        = null;
            Agent.UpcomingInteractionState = null;
            NPC._dialogue.EndChat = false;
        }
    }
}