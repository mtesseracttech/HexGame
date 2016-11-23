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
                            Debug.Log("Pathfinder gets: " + Player.GetCurrentNode().Index + " , " +
                                      hex.GetUnderlyingNode().Index);
                            _pathfinder = new Pathfinder();
                            _pathfinder.Search(Player.GetCurrentNode(), hex.GetUnderlyingNode());
                            _path = _pathfinder.Path;
                            if (_path != null)
                            {
                                if (_path.Count <= 2 && hex.GetUnderlyingNode().HasEnemy)
                                {
                                    Debug.Log("Has enemy, moving to closest tile");
                                    //SetWalkInfo
                                    //SetAttackInfo
                                }
                                else if (_path.Count == 1)
                                {
                                    Debug.Log("Moving to close tile");
                                    //SetWalkInfo
                                }
                            }
                        }
                    }
                }
            }
            //PSEUDOCODE:
            /*

            display available tiles
            Raycast mouseloc
            if(ray hit tag == selectabletile)
            {
                end = hit.tilemeh
                path = findPath(current, end)
                if(path.Count <= 2 && end.containsEnemy)
                {
                    show lines on path to enemy
                }
                elseif(path.Count = 1)
                {
                    show line on path
                }
                else
                {
                    Nothing ofc
                }
            }

            Raycast mouseclick
            if(ray hit tag == selectabletile)
            {
                end = hit.tilemeh
                path = findPath(current, end)
                if(path.Count <= 2 && end.containsEnemy)
                {
                    SetWalkInfo
                    SetAttackInfo
                }
                elseif(path.Count = 1)
                {
                    SetWalkInfo
                }
                else
                {
                    Nothing ofc
                    fml, end my suffering please
                }
            }

            fucking consumables
            */


            if (_path != null)
            {
                foreach (var hexNode in _path)
                {
                    try
                    {
                        Debug.DrawLine(hexNode.Position, hexNode.Parent.Position);
                    }
                    catch (Exception)
                    {
                    }

                }
            }


            if (Done)
            {
                Manager.ChangePhase(typeof(TurnPhasePlayerAction));
            }
            else
            {
                Debug.Log("Player Making Selection");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Player.IsIdling();
                    Done = true;
                }
            }
        }

        public override void Start()
        {
            Done = false;
            Player.SetState(typeof(PlayerStateIdle));
        }

        public override void End()
        {
        }
    }
}