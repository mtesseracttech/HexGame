using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.Agents
{
    public class PlayerAgent
    {
        private Dictionary<Type, PlayerStateBase> _states;
        private PlayerStateBase                   _currentState;
        private HexNode                           _targetNode;
        private HexNode                           _currentNode;
        private Pathfinder                        _pathfinder;
        private HexNode                           _pathEndNode;
        private List<HexNode>                     _path;
        private bool                              _doneMoving;
        private GameObject                        _gameObject;

        public PlayerAgent(GameObject gameObject)
        {
            _gameObject = gameObject;

            //Setting up the Cache
            _states = new Dictionary<Type, PlayerStateBase>();
            _states.Add(typeof(PlayerStateFreeMovement), new PlayerStateFreeMovement(this));
            _states.Add(typeof(PlayerStateStepMovement), new PlayerStateStepMovement(this));
            _states.Add(typeof(PlayerStateIdle),         new PlayerStateIdle        (this));

            //Starting first state manually
            _currentState = _states[typeof(PlayerStateIdle)];
            _currentState.BeginState();
        }

        public void UpdateState()
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

        public void GeneratePath(HexNode end)
        {
            _pathfinder.Search(_currentNode, end);
            _path = _pathfinder.Path;
        }

        public List<HexNode> GetPath()
        {
            return _path;
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

        public void SetCurrentNode(HexNode node)
        {
            _currentNode = node;
        }

        public Vector3 Position
        {
            get { return  _gameObject.transform.position; }
            set { _gameObject.transform.position = value; }
        }

        public Quaternion Rotation
        {
            get { return  _gameObject.transform.rotation; }
            set { _gameObject.transform.rotation = value; }
        }

        public bool IsIdling()
        {
            if (_currentState.GetType() == typeof(PlayerStateIdle))
            {
                return true;
            }
            return false;
        }
    }
}