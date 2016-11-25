using System;
using System.Collections.Generic;
using Assets.Scripts.AI.GameStep.FSM.FSMEnemy;
using Assets.Scripts.AI.GameStep.FSM.FSMPlayer;
using Assets.Scripts.AI.Pathfinding;
using Assets.Scripts.NodeGrid.Occupants.Specifics;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.Agents
{
    public class EnemyAgent : EnemyOccupant
    {
        private Dictionary<Type, EnemyStateBase>      _states;
        private EnemyStateBase                        _currentState;
        private HexNode                               _targetNode;
        private HexNode                               _attackTarget;
        private List<HexNode>                         _walkPath;
        private bool                                  _alive                 = true;

        public override void Start()
        {
            //Basics///////////////////////////////
            base.Start();
            Position = CurrentNode.Position;

            //Setting up the Cache/////////////////
            _states = new Dictionary<Type, EnemyStateBase>();
            _states.Add(typeof(EnemyStateStepMovement), new EnemyStateStepMovement(this));
            _states.Add(typeof(EnemyStateAttacking),    new EnemyStateAttacking   (this));
            _states.Add(typeof(EnemyStateIdle),         new EnemyStateIdle        (this));

            //Starting First State Manually////////
            _currentState = _states[typeof(EnemyStateIdle)];
            _currentState.BeginState();
        }

        //Update///////////////////////////////////
        void Update()
        {
            _currentState.Update();
            Debug.DrawLine(CurrentNode.Position, CurrentNode.Position + (Vector3.up*10) + Vector3.back, Color.yellow);
        }

        //State Related Methods////////////////////
        public void SetState(Type state)
        {
            if (_currentState.GetType() == state) return;
            Debug.Log("Enemy Changing from: " + _currentState.GetType() + " to: " + state);
            _currentState.EndState();
            _currentState = _states[state];
            _currentState.BeginState();
        }

        public bool IsIdling()
        {
            return _currentState.GetType() == typeof(PlayerStateIdle);
        }

        //Navigation Related///////////////////////
        public HexNode CurrentNode
        {
            get { return GetCurrentNode(); }
            set
            {
                SetCurrentNode  (value);
                Position = CurrentNode.Position;
            }
        }

        public HexNode TargetNode
        {
            get { return  _targetNode; }
            set { _targetNode = value; }
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

        //GameObject/Transform Related/////////////
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

        //FSM Related//////////////////////////////
        public bool IsDead()
        {
            return !_alive;
        }
    }
}