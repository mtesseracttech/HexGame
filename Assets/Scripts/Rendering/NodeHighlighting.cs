using Assets.Scripts.AI;
using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Rendering
{
    [RequireComponent(typeof(PlayerAgent))]
    public class NodeHighlighting : MonoBehaviour
    {
        private int _currentNodeIndex;
        private HexNode _currentNode;
        public GameObject NodeManager;
        private HexNodesManager _nodeManager;
        private BreadthFirst _pathfinder;
        private PlayerAgent _player;

        void Start ()
        {
            _nodeManager = NodeManager.GetComponent<HexNodesManager>();
            _pathfinder = GetComponent <BreadthFirst> ();
            _player = GetComponent<PlayerAgent>();
        }
	
        // Update is called once per frame
        void Update () 
        {
            UpdateCurrentPosition();
            if(Input.GetKeyDown(KeyCode.L))
            {
                //_pathfinder.ClearHighlights ();
                //StartCoroutine (_pathfinder.Search (_nodeManager.GetHexNode (CurrentNodeIndex)));
                //_nodeManager.GetHexNode (CurrentNodeIndex);
                OnGridShow();
            }

            if (Input.GetKeyDown (KeyCode.H)) {
                //	_pathfinder.ClearHighlights ();
                ClearGrid();
            }

            if (Input.GetKeyDown (KeyCode.A)) {
                _currentNodeIndex += 1;
                Debug.Log (_currentNodeIndex);
            }
        }

        public void SetCurrentPosition (int position) {
            _currentNodeIndex = position;
        }

        public void UpdateCurrentPosition()
        {
            _currentNodeIndex = _player.CurrentNode.Index;
        }

        public void OnGridShow()
        {
            StartCoroutine(_pathfinder.Search(_nodeManager.GetHexNode(_currentNodeIndex)));
        }

        public void ClearGrid()
        {
            _pathfinder.ClearHighlights();
        }
    }
}
