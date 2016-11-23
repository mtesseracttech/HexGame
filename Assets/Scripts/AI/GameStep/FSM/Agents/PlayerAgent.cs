using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.Agents
{
    public class PlayerAgent : MonoBehaviour
    {
        public  GameObject                        HexNodeManager;
        public  int                               StartNodeIndex;
        public  int                               EndNodeIndex;
        private Dictionary<Type, PlayerStateBase> _states;
        private PlayerStateBase                   _currentState;
        private HexNode                           _targetNode;
        private HexNode                           _currentNode;
        private Pathfinder                        _pathfinder;
        private List<HexNode>                     _path;
        private HexNodesManager                   _hexNodesManager;

        void Start()
        {
            if (HexNodeManager != null)
            {
                _hexNodesManager = HexNodeManager.GetComponent<HexNodesManager>();
                if (_hexNodesManager == null)
                {
                    Debug.Log("There was no HexNodesManager script bound to the given HexNodeManager instance!");
                }
                else
                {
                    Debug.Log("Successfully linked the HexNodeManager to the PlayerAgent!");
                    _currentNode = _hexNodesManager.GetHexNode(StartNodeIndex);
                    _targetNode  = _hexNodesManager.GetHexNode(EndNodeIndex);
                    transform.position = _currentNode.Position;

                }
            }
            else
            {
                Debug.Log("No HexNodeManager instance was supplied to the PlayerAgent!");
            }

            _pathfinder = new Pathfinder();


            //Setting up the Cache
            _states = new Dictionary<Type, PlayerStateBase>();
            _states.Add(typeof(PlayerStateFreeMovement), new PlayerStateFreeMovement(this));
            _states.Add(typeof(PlayerStateStepMovement), new PlayerStateStepMovement(this));
            _states.Add(typeof(PlayerStateIdle),         new PlayerStateIdle        (this));

            //Starting first state manually
            _currentState = _states[typeof(PlayerStateIdle)];
            _currentState.BeginState();
        }

        void Update()
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

        public void SetCurrentNode(HexNode node)
        {
            _currentNode = node;
        }

        public Vector3 Position
        {
            get { return  transform.position; }
            set { transform.position = value; }
        }

        public Quaternion Rotation
        {
            get { return  transform.rotation; }
            set { transform.rotation = value; }
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