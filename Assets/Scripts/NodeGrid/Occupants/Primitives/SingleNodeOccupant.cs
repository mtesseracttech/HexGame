using Assets.Scripts.AI.Pathfinding;

namespace Assets.Scripts.NodeGrid.Occupants.Primitives
{
    public abstract class SingleNodeOccupant : NodeOccupant
    {
        public    int     StartNodeIndex;
        protected HexNode PrCurrentNode;

        public virtual void Start()
        {
            FillCurrentNode();
        }

        private void FillCurrentNode()
        {
            SetCurrentNode(Manager.GetHexNode(StartNodeIndex));
        }

        public void SetCurrentNode(HexNode targetNode)
        {
            if (PrCurrentNode != null) PrCurrentNode.Occupant = null;
            PrCurrentNode = targetNode;
            PrCurrentNode.Occupant = this;
        }

        public HexNode GetCurrentNode()
        {
            return PrCurrentNode;
        }
    }
}