using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.NodeGrid.Occupants.Primitives
{
    public abstract class SingleNodeOccupant : NodeOccupant
    {
        public    int     StartNodeIndex;
        public    bool    SnapToClosestNode = true;
        protected HexNode PrCurrentNode;

        public virtual void Start()
        {
            FillCurrentNode();
        }

        private void FillCurrentNode()
        {
            if (SnapToClosestNode)
            {
                SetCurrentNode(Manager.ReturnClosestHexNode(transform.position));
            }
            else
            {
                SetCurrentNode(Manager.GetHexNode(StartNodeIndex));
            }

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