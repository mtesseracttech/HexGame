using System;
using System.Collections.Generic;

namespace Assets.Scripts.AI
{
    public class PlayerActor
    {
        private Dictionary<Type, PlayerStateBase> _states;
        private PlayerStateBase _currentState;

        public PlayerActor()
        {
            _states.Add(typeof(PlayerStateFreeMovement), new PlayerStateFreeMovement(this));
            _states.Add(typeof(PlayerStateStepMovement), new PlayerStateStepMovement(this));
            _states.Add(typeof(PlayerStateWaiting),      new PlayerStateWaiting(this)     );
        }

        public void Update()
        {
            _currentState.Update();
        }
    }
}