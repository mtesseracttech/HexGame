using System;
using System.Collections.Generic;
using Assets.Scripts.AI;
using Assets.Scripts.AI.GameStep.FSM.Agents;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhasePlayerSelection : TurnPhasePlayerBase
    {
        private Pathfinder _pathfinder;
        private List<HexNode> _path;
        private BreadthFirst _breathFirstfinder;

        public TurnPhasePlayerSelection(TurnManager manager, PlayerAgent player) : base(manager, player)
        {
            Player = player;
            _pathfinder = new Pathfinder();
            _breathFirstfinder = new BreadthFirst();
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
                            _pathfinder = new Pathfinder();
                            _pathfinder.Search(Player.CurrentNode, hex.GetUnderlyingNode());
                            _path = _pathfinder.Path;
                            if (_path != null)
                            {
                                if (_path.Count <= 2 && hex.GetUnderlyingNode().HasEnemy)
                                {
                                    Debug.Log("Has enemy, moving to closest tile");
                                    List<HexNode> tempPath = new List<HexNode>();
                                    tempPath.Add(_path[0]);
                                    Manager.WalkPath = tempPath;
                                    Manager.AttackTarget = hex.GetUnderlyingNode();
                                    Done = true;

                                }
                                else if (_path.Count == 1)
                                {
                                    Debug.Log("Moving to close tile");
                                    List<HexNode> tempPath = new List<HexNode>();
                                    tempPath.Add(_path[0]);
                                    Manager.WalkPath = tempPath;
                                    Manager.AttackTarget = null;
                                    Done = true;
                                }
                                else
                                {
                                    Debug.Log("That tile is too far away to walk to/attack!");
                                }
                            }
                        }
                    }
                }
            }
            //fucking consumables

            if (Done)
            {
                Manager.ChangePhase(typeof(TurnPhasePlayerAction));
            }
        }

        public override void Start()
        {
            Done                 = false;
            Manager.AttackTarget = null;
            Manager.WalkPath     = null;
            Player.SetState(typeof(PlayerStateIdle));
            //show the highlights
        }

        public override void End()
        {
            _breathFirstfinder.ClearHighlights();
        }
    }
}