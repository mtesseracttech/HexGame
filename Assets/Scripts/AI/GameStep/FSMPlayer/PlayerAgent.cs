using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class PlayerAgent// : MonoBehaviour
    {
        private Dictionary<Type, PlayerStateBase> _states;
        private PlayerStateBase                   _currentState;
        private HexNode                           _targetNode;
        private HexNode                           _currentNode;
        private Pathfinder                        _pathfinder;
        private HexNode                           _pathEndNode;
        private List<HexNode>                     _path;
        private bool                              _doneMoving;


        public PlayerAgent()
        {
            _states = new Dictionary<Type, PlayerStateBase>();

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

        public void GetPath(HexNode end)
        {
            _pathfinder.Search(_currentNode, end);
            _path = _pathfinder.Path;
        }

        public HexNode GetCurrentNode()
        {
            return _currentNode;
        }

        public HexNode GetCurrentTarget()
        {
            return _targetNode;
        }

        public HexNode GetCurrentEndPoint()
        {
            return _pathEndNode;
        }

        public bool IsDoneMoving()
        {
            return _doneMoving;
        }
    }
}