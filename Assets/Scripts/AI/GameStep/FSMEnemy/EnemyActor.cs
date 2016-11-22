using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSMEnemy
{
    public class EnemyActor
    {
        private Dictionary<Type, EnemyStateBase> _states;
        private EnemyStateBase _currentState;

        public EnemyActor()
        {
            _states.Add(typeof(PlayerStateStepMovement), new EnemyStateStepMovement(this));
            _states.Add(typeof(PlayerStateIdle),         new EnemyStateIdle        (this));
            _currentState = _states[typeof(EnemyStateIdle)];
        }

        public void Update()
        {
            _currentState.Update();
        }

        public void SetState(Type state)
        {
            Debug.Log("Player Changing from: " + _currentState.GetType() + " to: " + state);
            _currentState.EndState();
            _currentState = _states[state];
            _currentState.BeginState();
        }
    }
}