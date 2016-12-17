using Assets.Scripts.NodeGrid.Occupants.Primitives;

namespace Assets.Scripts.NodeGrid.Occupants.Specifics
{
    public abstract class PropOccupant : SingleNodeOccupant
    {
       public abstract void Interact();
    }
}