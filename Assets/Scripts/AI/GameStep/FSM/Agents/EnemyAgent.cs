using System;
using System.Collections.Generic;
using Assets.Scripts.AI.GameStep.FSMEnemy;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.Agents
{
    public class EnemyAgent : MonoBehaviour
    {
        private Dictionary<Type, EnemyStateBase> _states;
        private EnemyStateBase                   _currentState;
        private HexNode                          _targetNode;
        private HexNode                          _currentNode;
        private Pathfinder                       _pathfinder;
        private List<HexNode>                    _path;
        private HexNodesManager                  _hexNodesManager;
        private HexNode                          _attackTarget;
        private bool                             _alive            = true;

        void Start()
        {
            _states = new Dictionary<Type, EnemyStateBase>();

            _states.Add(typeof(EnemyStateStepMovement), new EnemyStateStepMovement(this));
            _states.Add(typeof(EnemyStateAttacking),    new EnemyStateAttacking   (this));
            _states.Add(typeof(EnemyStateIdle),         new EnemyStateIdle        (this));

            _currentState = _states[typeof(EnemyStateIdle)];
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
            return _currentState.GetType() == typeof(PlayerStateIdle);
        }

    }
}