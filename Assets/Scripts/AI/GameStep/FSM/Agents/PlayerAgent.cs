using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.Agents
{
    public class PlayerAgent : MonoBehaviour
    {
        public  GameObject                        HexNodeManager;
        public  int                               StartNodeIndex;
        private Dictionary<Type, PlayerStateBase> _states;
        private PlayerStateBase                   _currentState;
        private HexNode                           _targetNode;
        private HexNode                           _currentNode;
        private Pathfinder                        _pathfinder;
        private List<HexNode>                     _path;
        private HexNodesManager                   _hexNodesManager;
        private HexNode                           _attackTarget;

        void Start()
        {
            string startDebug = "Player Start Debug Info:\n";
            if (HexNodeManager != null)
            {
                _hexNodesManager = HexNodeManager.GetComponent<HexNodesManager>();
                if (_hexNodesManager == null)
                {
                    startDebug += "There was no HexNodesManager script bound to the given HexNodeManager instance!\n";
                }
                else
                {
                    Debug.Log("Successfully linked the HexNodeManager to the PlayerAgent!");
                    bool spawnSuccess = SetSpawn(_hexNodesManager.GetHexNode(StartNodeIndex));
                    if (spawnSuccess)
                    {
                        startDebug += "Successfully managed to spawn the player on Node "
                                      + _currentNode.Index + "at position: " + _currentNode.Position + "\n";
                    }
                    else
                    {
                        startDebug += "Failed to spawn the player on Node " + _currentNode.Index + "at position: " +
                                  _currentNode.Position+ "\n";
                    }
                }
            }
            else
            {
                startDebug += "No HexNodeManager instance was supplied to the PlayerAgent!\n";
            }

            _pathfinder = new Pathfinder();


            //Setting up the Cache
            _states = new Dictionary<Type, PlayerStateBase>();
            _states.Add(typeof(PlayerStateFreeMovement), new PlayerStateFreeMovement(this));
            _states.Add(typeof(PlayerStateStepMovement), new PlayerStateStepMovement(this));
            _states.Add(typeof(PlayerStateAttack),       new PlayerStateAttack      (this));
            _states.Add(typeof(PlayerStateIdle),         new PlayerStateIdle        (this));

            //Starting first state manually
            _currentState = _states[typeof(PlayerStateIdle)];
            _currentState.BeginState();

            Debug.Log(startDebug);
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

        public HexNode CurrentNode
        {
            get { return  _currentNode; }
            set
            {
                _currentNode = value;
                Position = value.Position;
            }
        }

        public HexNode TargetNode
        {
            get { return  _targetNode; }
            set { _targetNode = value; }
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

        public HexNode AttackTarget
        {
            get { return  _attackTarget; }
            set { _attackTarget = value; }
        }

        public bool IsIdling()
        {
            if (_currentState.GetType() == typeof(PlayerStateIdle))
            {
                return true;
            }
            return false;
        }

        public bool SetSpawn(HexNode spawnNode)
        {
            if (!spawnNode.IsOccupiedByAnything())
            {
                _currentNode = spawnNode;
                Position = _currentNode.Position;
                return true;
            }
            return false;
        }
    }
}