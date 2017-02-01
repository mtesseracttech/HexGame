using Assets.Scripts.NodeGrid.Occupants.Primitives;

namespace Assets.Scripts.NodeGrid.Occupants.Specifics
{
    public class NPCOccupant : SingleNodeOccupant
    {
        private bool _interactionDone;

        public void Interact()
        {
            _interactionDone = false;
        }

        public bool GetInteractionOver()
        {
            return _interactionDone;
        }
    }
}