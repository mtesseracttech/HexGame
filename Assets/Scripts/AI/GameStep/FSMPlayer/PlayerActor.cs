﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class PlayerActor : MonoBehaviour
    {
        private Dictionary<Type, PlayerStateBase> _states;
        private PlayerStateBase _currentState;

        public PlayerActor()
        {
            _states.Add(typeof(PlayerStateFreeMovement), new PlayerStateFreeMovement(this));
            _states.Add(typeof(PlayerStateStepMovement), new PlayerStateStepMovement(this));
            _states.Add(typeof(PlayerStateIdle),         new PlayerStateIdle        (this));
            _currentState = _states[typeof(PlayerStateIdle)];
        }

        public void Update()
        {
            _currentState.Update();
        }

        public void SetState(Type state)
        {
            if (_currentState.GetType() == state) return;
            Debug.Log("Player Changing from: " + _currentState.GetType() + " to: " + state);
            _currentState.EndState();
            _currentState = _states[state];
            _currentState.BeginState();
        }
    }
}