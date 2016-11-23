using System;
using System.Collections.Generic;
using Assets.Scripts.AI.GameStep.FSMEnemy;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.Agents
{
    public class EnemyAgent
    {
        private Dictionary<Type, EnemyStateBase> _states;
        private EnemyStateBase _currentState;

        public EnemyAgent()
        {
            _states = new Dictionary<Type, EnemyStateBase>();

            _states.Add(typeof(EnemyStateStepMovement), new EnemyStateStepMovement(this));
            _states.Add(typeof(EnemyStateAttacking),    new EnemyStateAttacking   (this));
            _states.Add(typeof(EnemyStateIdle),         new EnemyStateIdle        (this));

            _currentState = _states[typeof(EnemyStateIdle)];
        }

        public void Update()
        {
            _currentState.Update();
        }

        public void SetState(Type state)
        {
            if (_currentState.GetType() == state) return;
            Debug.Log("Enemy Changing from: " + _currentState.GetType() + " to: " + state);
            _currentState.EndState();
            _currentState = _states[state];
            _currentState.BeginState();
        }
    }
}