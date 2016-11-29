using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    class TurnPhaseEnemyRotation : TurnPhaseBase
    {
        private bool _returnToIdle = false;

        public TurnPhaseEnemyRotation(TurnManager manager) : base(manager)
        {
        }

        public override void Update()
        {
            if (Manager.HasNextEnemy()) _returnToIdle = false;
            else _returnToIdle = true;

            if (!_returnToIdle)
            {
                Manager.SetNextEnemy();
                Manager.ChangePhase(typeof(TurnPhaseEnemySelection));
            }
            else
            {
                Manager.ChangePhase(typeof(TurnPhaseIdle));
            }
        }
                
        public override void Start()
        {

        }

        public override void End()
        {
        }
    }
}
