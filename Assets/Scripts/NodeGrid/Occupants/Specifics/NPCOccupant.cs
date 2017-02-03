using Assets.Scripts.NodeGrid.Occupants.Primitives;
using Assets.Scripts.NPC;
using UnityEngine;

namespace Assets.Scripts.NodeGrid.Occupants.Specifics
{
    public class NPCOccupant : SingleNodeOccupant
    {
        public DialogueSystema _dialogue;

        private bool _interactionDone;

        public void Interact()
        {
            _interactionDone = false;
           if (_dialogue.EndChat)
            {
                _interactionDone = true;
            }
                
        }

        public bool GetInteractionOver()
        {
            return _interactionDone;
        }

        
    }
}