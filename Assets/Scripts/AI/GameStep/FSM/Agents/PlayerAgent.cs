using System;
using System.Collections.Generic;
using Assets.Scripts.AI.GameStep.FSM.FSMPlayer;
using Assets.Scripts.AI.Pathfinding;
using Assets.Scripts.Inventory.Stats;
using Assets.Scripts.NodeGrid.Occupants.Specifics;
using Assets.Scripts.Rendering;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.Agents
{
    public class PlayerAgent : PlayerOccupant
    {
        //Public Variables/////////////////////////
        public  int                                   HighlighterRadius           = 5;
        public  GameObject                            CombatUi;
        public  CoinFlip                              CoinFlip;
        
        //Private Variables////////////////////////
        private Dictionary<Type, PlayerStateBase>     _states;
        private PlayerStateBase                       _currentState;
        private HexNode                               _targetNode;
        private HexNode                               _interactionTarget;
        private List<HexNode>                         _walkPath;
        private ProperHighlighter                     _properHighlighter;
        private Type                                  _upcomingInteractionState;
        private Animator                              _animator;

        public override void Start()
        {
            //Basics///////////////////////////////
            base.Start();
            Position = CurrentNode.Position;
            _properHighlighter = GetComponent <ProperHighlighter>();
            _animator = GetComponentInChildren<Animator>();

            //Setting up the Cache/////////////////
            _states = new Dictionary<Type, PlayerStateBase>();
            _states.Add(typeof(PlayerStateIdle),                new PlayerStateIdle                (this));
            _states.Add(typeof(PlayerStateWalking),             new PlayerStateWalking             (this));
            _states.Add(typeof(PlayerStateInteractionEnemy),    new PlayerStateInteractionEnemy    (this));
            _states.Add(typeof(PlayerStateInteractionBuilding), new PlayerStateInteractionBuilding (this));
            _states.Add(typeof(PlayerStateInteractionNPC),      new PlayerStateInteractionNPC      (this));
            _states.Add(typeof(PlayerStateInteractionProp),     new PlayerStateInteractionProp     (this));

            //Starting First State Manually////////
            _currentState = _states[typeof(PlayerStateIdle)];
            _currentState.BeginState();
        }

        //Update///////////////////////////////////
        void Update()
        {
            _currentState.Update();
        }

        //State Related Methods////////////////////
        public void SetState(Type state)
        {
            if (_currentState.GetType() == state) return;
            Debug.Log("Player Changing from: " + _currentState.GetType() + " to: " + state);
            _currentState.EndState();
            _currentState = _states[state];
            _currentState.BeginState();
        }

        public bool IsIdling()
        {
            return _currentState.GetType() == typeof(PlayerStateIdle);
        }

        public Type UpcomingInteractionState
        {
            get { return  _upcomingInteractionState; }
            set { _upcomingInteractionState = value; }
        }

        public void ShowHighLight(bool show, bool freeMovement = false)
        {
            if (show) _properHighlighter.ShowHighlight(CurrentNode, HighlighterRadius, freeMovement);
            else _properHighlighter.DestroyHighlights();
        }

        //Navigation Related///////////////////////
        public HexNode CurrentNode
        {
            get { return GetCurrentNode(); }
            set
            {
                SetCurrentNode(value);
                Position = CurrentNode.Position;
            }
        }

        public HexNode InteractionTarget
        {
            get { return  _interactionTarget; }
            set { _interactionTarget = value; }
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

        public void SetWalkAnimation(bool walk)
        {
            _animator.SetBool("IsWalking", walk);
        }

    }
}