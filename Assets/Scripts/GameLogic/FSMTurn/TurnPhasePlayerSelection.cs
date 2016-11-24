﻿using System;
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
                            _pathfinder = new Pathfinder();
                            _pathfinder.Search(Player.CurrentNode, hex.GetUnderlyingNode());
                            _path = _pathfinder.Path;
                            if (_path != null)
                            {
                                if (_path.Count <= 2 && hex.GetUnderlyingNode().HasEnemy)
                                {
                                    Debug.Log("Has enemy, moving to closest tile");
                                    List<HexNode> tempPath = new List<HexNode>();
                                    if(_path.Count == 2) tempPath.Add(_path[0]);
                                    Manager.WalkPath = tempPath;
                                    Manager.AttackTarget = hex.GetUnderlyingNode();
                                    Manager.ChangePhase(typeof(TurnPhasePlayerAction));

                                }
                                else if (_path.Count == 1)
                                {
                                    Debug.Log("Moving to close tile");
                                    List<HexNode> tempPath = new List<HexNode>();
                                    tempPath.Add(_path[0]);
                                    Manager.WalkPath = tempPath;
                                    Manager.AttackTarget = null;
                                    Manager.ChangePhase(typeof(TurnPhasePlayerAction));
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
        }

        public override void Start()
        {
            Manager.AttackTarget = null;
            Manager.WalkPath     = null;
            Player.SetState(typeof(PlayerStateIdle));
            //show the highlights
        }

        public override void End()
        {
            _breathFirstfinder.ClearHighlights();

            //Here is where the highlights should be disabled!

        }
    }
}