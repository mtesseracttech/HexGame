using Assets.Scripts.AI;
using Assets.Scripts.AI.GameStep.FSM.Agents;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhasePlayerSelection : TurnPhasePlayerBase
    {
        public TurnPhasePlayerSelection(TurnManager manager, PlayerAgent player) : base(manager, player)
        {
            Player = player;
        }

        public override void Update()
        {
            if (Done)
            {
                Manager.ChangePhase(typeof(TurnPhasePlayerAction));
            }
            else
            {
                Debug.Log("Player Making Selection");
                if (Input.GetKeyDown(KeyCode.Space))
                {
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