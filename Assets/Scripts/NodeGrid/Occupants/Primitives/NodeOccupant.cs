using UnityEngine;

namespace Assets.Scripts.NodeGrid.Occupants.Primitives
{
    public abstract class NodeOccupant : MonoBehaviour
    {
        public GameObject HexNodeManager;
        protected HexNodesManager Manager;

        public virtual void Awake()
        {
            Manager = HexNodeManager.GetComponent<HexNodesManager>();
        }
    }
}