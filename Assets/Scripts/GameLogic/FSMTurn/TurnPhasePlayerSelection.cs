using System.Collections.Generic;
using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.GameStep.FSM.FSMPlayer;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhasePlayerSelection : TurnPhasePlayerBase
    {
        private bool _finishedSelection;

        public TurnPhasePlayerSelection(TurnManager manager, PlayerAgent player) : base(manager, player) {}

        public override void Update()
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            MouseClick(mouseRay);

            if (_finishedSelection)
            {
                Manager.ChangePhase(typeof(TurnPhasePlayerAction));
            }
        }

        private void MouseClick(Ray mouseRay)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(Player.CurrentNode.Index);
                RaycastHit mouseClickHit;

                if (Physics.Raycast(mouseRay, out mouseClickHit))
                {
                    if (mouseClickHit.collider.CompareTag("selectionhex"))
                    {
                        var hex = mouseClickHit.transform.GetComponent<SelectionHexRenderer>();

                        if (hex != null)
                        {
                            HexNode hexNode = hex.GetUnderlyingNode();

                            Manager.Pathfinder.Search(Player.CurrentNode, hexNode);
                            Debug.Log(Manager.Pathfinder);
                            List<HexNode> path = Manager.Pathfinder.Path;

                            if (path != null) SelectInteraction(path, hexNode);
                        }
                    }
                }
            }
        }

        private void SelectInteraction(List<HexNode> path, HexNode target)
        {
            Debug.Log("Path Length is: " + path.Count);
            if (path.Count > 2)
            {
                Debug.Log("The path that was found is too long for interactions");
            }
            else if (path.Count == 2)
            {

                if (target.HasOccupant)
                {
                    Debug.Log("Player will have to walk and then do interaction");
                    SetInteractionState(target);
                    Player.WalkPath = new List<HexNode> {path[0]};
                    _finishedSelection = true;
                }
                else
                {
                    Debug.Log("This is too far for an occupant-less node!");
                }
            }
            else if (path.Count == 1)
            {
                Debug.Log("Player will interact directly");
                if (target.HasOccupant) SetInteractionState(target);
                else Player.WalkPath = new List<HexNode> {path[0]};

                _finishedSelection = true;
            }
            else
            {
                Debug.Log("The selected tile was the player itself, you idiot");
            }
        }

        private void SetInteractionState(HexNode target)
        {
            Player.InteractionTarget = target;

            if (target.HasEnemy)
            {
                Player.UpcomingInteractionState = typeof(PlayerStateInteractionEnemy);
            }
            else if (target.HasNPC)
            {
                Player.UpcomingInteractionState = typeof(PlayerStateInteractionNPC);
            }
            else if (target.HasBuilding)
            {
                Player.UpcomingInteractionState = typeof(PlayerStateInteractionBuilding);
            }
            else if (target.HasProp)
            {
                Player.UpcomingInteractionState = typeof(PlayerStateInteractionProp);
            }
            else
            {
                Player.InteractionTarget = null;
                Debug.Log("Player got desynced somehow!");
            }

            Debug.Log("Selected next phase: " + Player.UpcomingInteractionState);
        }

        public override void Start()
        {
            _finishedSelection              = false;
            Player.InteractionTarget        = null;
            Player.UpcomingInteractionState = null;
            Player.WalkPath                 = null;
            Player.ShowHighLight (true);
        }

        public override void End()
        {
            Player.ShowHighLight (false);
        }
    }
}