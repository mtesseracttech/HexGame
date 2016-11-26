using Assets.Scripts.AI.Pathfinding;

namespace Assets.Scripts.NodeGrid.Occupants.Primitives
{
    public abstract class MultiNodeOccupant : NodeOccupant
    {
        public int[] StartNodesIndexes;
        protected HexNode[] CurrentNodes;

        public virtual void Start()
        {
            CurrentNodes = new HexNode[StartNodesIndexes.Length];
            FillCurrentNodes();
        }

        private void FillCurrentNodes()
        {
            HexNode[] tempNodes = new HexNode[StartNodesIndexes.Length];
            for (int i = 0; i < StartNodesIndexes.Length; i++)
            {
                tempNodes[i] = Manager.GetHexNode(StartNodesIndexes[i]);
            }
            SetCurrentNodes(tempNodes);
        }

        public void SetCurrentNodes(HexNode[] targetNodes)
        {
            for (int i = 0; i < CurrentNodes.Length; i++)
            {
                if(CurrentNodes[i] != null) CurrentNodes[i].Occupant = null;
                CurrentNodes[i] = targetNodes[i];
                CurrentNodes[i].Occupant = this;
            }
        }
    }
}