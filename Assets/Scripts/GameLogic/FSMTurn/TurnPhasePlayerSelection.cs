using System;
using System.Collections.Generic;
using Assets.Scripts.AI;
using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.GameStep.FSM.FSMPlayer;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhasePlayerSelection : TurnPhasePlayerBase
    {
        private Pathfinder _pathfinder;
        private List<HexNode> _path;
        private List<HexNode> _debugPath;
        private HexNode _debugCurrentNode;

        public TurnPhasePlayerSelection(TurnManager manager, PlayerAgent player) : base(manager, player)
        {
            Player = player;
            _pathfinder = new Pathfinder();
        }

        public override void Update()
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("selectiontile"))
                {
                    SelectionHexRenderer hex = hit.collider.gameObject.GetComponent<SelectionHexRenderer>();
                    if (hex != null)
                    {
                        Debug.Log("Hovering over selection tile! Connected node: Index: " +
                                  hex.GetUnderlyingNode().Index + " at " +
                                  hex.GetUnderlyingNode().Position);
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit clickHit;
                Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(clickRay, out clickHit))
                {
                    if (hit.collider.CompareTag("selectiontile"))
                    {
                        SelectionHexRenderer hex = hit.collider.gameObject.GetComponent<SelectionHexRenderer>();
                        if (hex != null)
                        {
                            //_debugCurrentNode = Player.CurrentNode;
                            _pathfinder = new Pathfinder();
                            _pathfinder.Search(Player.CurrentNode, hex.GetUnderlyingNode());

                            _path = _pathfinder.Path;
                            if (_path != null)
                            {
                                if (_path.Count <= 2 && hex.GetUnderlyingNode().HasEnemy)
                                {
                                    Debug.Log("Has enemy, moving to closest tile");
                                    if (_path.Count == 2)
                                    {
                                        List<HexNode> tempPath = new List<HexNode> {_path[0]};
                                        Player.WalkPath = tempPath;
                                    }
                                    Player.AttackTarget = hex.GetUnderlyingNode();
                                    Manager.ChangePhase(typeof(TurnPhasePlayerAction));

                                }
                                else if (_path.Count == 1)
                                {
                                    Debug.Log("Moving to close tile");
                                    List<HexNode> tempPath = new List<HexNode>();
                                    tempPath.Add(_path[0]);
                                    Player.WalkPath = tempPath;
                                    Player.AttackTarget = null;
                                    Manager.ChangePhase(typeof(TurnPhasePlayerAction));
                                }
                                else
                                {
                                    //Debug.Log(_path.Count);
                                    //DebugPath(_path);
                                    Debug.Log("That tile is too far away to walk to/attack!");
                                }
                            }
                        }
                    }
                }
            }
            //fucking consumables

            /*
            if (_debugPath != null)
            {
                foreach (var node in _debugPath)
                {
                    if (node.Parent != null)
                    {
                        Debug.DrawLine(node.Position, node.Parent.Position, Color.cyan);
                    }
                }
            }

            if (_debugCurrentNode != null)
            {
                Debug.DrawLine(_debugCurrentNode.Position, _debugCurrentNode.Position + Vector3.up*20, Color.magenta);
            }
            */
        }

        private void DebugPath(List<HexNode> path)
        {
            string pathIndexes = "Debug Path Indexes: \n";
            _debugPath = path;
            foreach (var node in path)
            {
                pathIndexes += node.Index + " ";
            }
            Debug.Log(pathIndexes);
        }


        public override void Start()
        {
            Player.AttackTarget = null;
            Player.WalkPath     = null;
            Player.SetState(typeof(PlayerStateIdle));
            Player.ShowHighLight(true);
        }

        public override void End()
        {
            Player.ShowHighLight(false);
        }
    }
}