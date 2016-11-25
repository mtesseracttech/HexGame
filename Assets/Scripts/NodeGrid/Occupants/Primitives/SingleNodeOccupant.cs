using Assets.Scripts.AI.Pathfinding;

namespace Assets.Scripts.NodeGrid.Occupants.Primitives
{
    public abstract class SingleNodeOccupant : NodeOccupant
    {
        public int StartNodeIndex;
        protected HexNode prCurrentNode;

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
            if (prCurrentNode != null)
            {
                if (prCurrentNode.Occupant is SingleNodeOccupant)
                {
                    SingleNodeOccupant temp = prCurrentNode.Occupant as SingleNodeOccupant;
                    temp.SetCurrentNode(null);
                }
                prCurrentNode.Occupant = null;
            }
            prCurrentNode = targetNode;
            prCurrentNode.Occupant = this;
        }

        public HexNode GetCurrentNode()
        {
            return prCurrentNode;
        }
    }
}