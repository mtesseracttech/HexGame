using System;
using System.Collections.Generic;
using Assets.Scripts.AI.GameStep.FSMEnemy;
using Assets.Scripts.GameLogic.FSMTurn;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.Agents
{
    public class EnemyAgent : MonoBehaviour
    {
        public  GameObject                        HexNodeManager;
        public  int                               StartNodeIndex;
        private Dictionary<Type, EnemyStateBase> _states;
        private EnemyStateBase                   _currentState;
        private HexNode                          _targetNode;
        private HexNode                          _currentNode;
        private Pathfinder                       _pathfinder;
        private List<HexNode>                    _path;
        private HexNodesManager                  _hexNodesManager;
        private HexNode                          _attackTarget;
        private List<HexNode>                    _walkPath;
        private bool                             _alive            = true;

        void Start()
        {
            string startDebug = "EnemyAgent Start Debug Info:\n";
            if (HexNodeManager != null)
            {
                _hexNodesManager = HexNodeManager.GetComponent<HexNodesManager>();
                if (_hexNodesManager == null)
                {
                    startDebug += "There was no HexNodesManager script bound to the given HexNodeManager instance!\n";
                }
                else
                {
                    startDebug += "Successfully linked the HexNodeManager to the EnemyAgent!\n";
                    bool spawnSuccess = SetSpawn(_hexNodesManager.GetHexNode(StartNodeIndex));
                    if (spawnSuccess)
                    {
                        startDebug += "Successfully managed to spawn the enemy on Node "
                                      + _currentNode.Index + "at position: " + _currentNode.Position + "\n";
                    }
                    else
                    {
                        startDebug += "Failed to spawn the enemy on Node " + _currentNode.Index + "at position: " +
                                      _currentNode.Position+ "\n";
                    }
                }
            }
            else
            {
                startDebug += "No HexNodeManager instance was supplied to the EnemyAgent!\n";
            }

            _states = new Dictionary<Type, EnemyStateBase>();

            _states.Add(typeof(EnemyStateStepMovement), new EnemyStateStepMovement(this));
            _states.Add(typeof(EnemyStateAttacking),    new EnemyStateAttacking   (this));
            _states.Add(typeof(EnemyStateIdle),         new EnemyStateIdle        (this));

            _currentState = _states[typeof(EnemyStateIdle)];
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
            Debug.Log("Enemy Changing from: " + _currentState.GetType() + " to: " + state);
            _currentState.EndState();
            _currentState = _states[state];
            _currentState.BeginState();
        }

        public bool IsDead()
        {
            return !_alive;
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

        public HexNode CurrentNode
        {
            get { return  _currentNode; }
            set
            {
                _currentNode = value;
                _currentNode.HasEnemy = true;
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

        public bool IsIdling()
        {
            return _currentState.GetType() == typeof(PlayerStateIdle);
        }

        public HexNode AttackTarget
        {
            get { return  _attackTarget; }
            set { _attackTarget = value; }
        }

        public List<HexNode> WalkPath
        {
            get { return  _walkPath; }
            set { _walkPath = value; }
        }

    }
}