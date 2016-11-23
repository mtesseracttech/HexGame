
using Assets.Scripts.AI;
using Assets.Scripts.AI.GameStep.FSM.Agents;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhasePlayerAction : TurnPhasePlayerBase
    {
        public TurnPhasePlayerAction(TurnManager manager, PlayerAgent player) : base(manager, player)
        {
        }

        public override void Update()
        {
            Debug.Log("Player Action Time");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Manager.ChangePhase(typeof(TurnPhaseEnemySelection));
            }
        }

        public override void Start()
        {
            Done = false;
            Player.SetState(typeof(PlayerStateStepMovement));
        }

        public override void End()
        {
        }
    }
}