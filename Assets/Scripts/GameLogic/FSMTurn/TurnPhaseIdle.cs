using Assets.Scripts.AI;
using Assets.Scripts.AI.GameStep.FSM.FSMEnemy;
using Assets.Scripts.AI.GameStep.FSM.FSMPlayer;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhaseIdle : TurnPhaseBase
    {
        private bool _exitingIdle;

        public TurnPhaseIdle(TurnManager manager) : base(manager) {}

        public override void Update()
        {
            Debug.Log("Idling!");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _exitingIdle = true;
            }

            if (_exitingIdle)
            {
                Manager.ChangePhase(typeof(TurnPhasePlayerSelection));
            }
        }

        public override void Start()
        {
            _exitingIdle = false;
        }

        public override void End()
        {

        }
    }
}