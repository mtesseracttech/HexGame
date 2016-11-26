using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.Pathfinding;
using Assets.Scripts.Rendering;
using UnityEngine;

namespace Assets.Scripts.NodeGrid
{
    public class PlayerNodeHighlight : MonoBehaviour
    {
        private HexNode             _currentNode;
        private NodeHighlighter     _nodeHighlighter;
        private PlayerAgent         _player;

        void Start ()
        {
            _nodeHighlighter = GetComponent <NodeHighlighter> ();
            _player = GetComponent<PlayerAgent>();
        }

        public void OnGridShow()
        {
            StartCoroutine(_nodeHighlighter.Search(_player.CurrentNode));
        }

        public void ClearGrid()
        {
            _nodeHighlighter.ClearHighlights();
        }
    }
}