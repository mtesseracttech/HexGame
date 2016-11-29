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
        private Dictionary<Type, PlayerStateBase>     _states;
        private PlayerStateBase                       _currentState;
        private HexNode                               _targetNode;
        private HexNode                               _attackTarget;
        private List<HexNode>                         _walkPath;
        private NodeHighlighter                       _nodeHighlighter;
        public GameObject CombatUI;
        public CoinFlip flipCoin;

        public override void Start()
        {
            //Basics///////////////////////////////
            base.Start();
            Position = CurrentNode.Position;
            _nodeHighlighter = GetComponent <NodeHighlighter> ();

            //Setting up the Cache/////////////////
            _states = new Dictionary<Type, PlayerStateBase>();
            _states.Add(typeof(PlayerStateFreeMovement), new PlayerStateFreeMovement(this));
            _states.Add(typeof(PlayerStateStepMovement), new PlayerStateStepMovement(this));
            _states.Add(typeof(PlayerStateAttack),       new PlayerStateAttack      (this));
            _states.Add(typeof(PlayerStateIdle),         new PlayerStateIdle        (this));

            //Starting First State Manually////////
            _currentState = _states[typeof(PlayerStateIdle)];
            _currentState.BeginState();
        }

        //Update///////////////////////////////////
        void Update()
        {
            _currentState.Update();
            Debug.DrawLine(CurrentNode.Position, CurrentNode.Position + (Vector3.up*10) + Vector3.back, Color.red);
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

        //Highlight Grid Related Methods///////////
        public void OnGridShow()
        {
            StartCoroutine(_nodeHighlighter.Search(CurrentNode));
        }

        public void ClearGrid()
        {
            _nodeHighlighter.ClearHighlights();
        }

        public void ShowHighLight(bool show)
        {
            if (show)OnGridShow();
            else ClearGrid();
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

        //attacking phase related
        public void AttackingEnemy(bool attack)
        {
            if (attack)
            {
                //show button attack
                //if pressed attack
                //flip coin
                //then show result of coins flipping
                //then display stats above player and enemy what attack and defense they have
                //show how much damage done like 0 and 2.
                //attack becomes false
               CombatUI.SetActive(true);
                //check if button pressed if so then go to idle state
            }
            else
            {
                CombatUI.SetActive(false);
            }
        }

        public bool CoinFlipCheck()
        {
            return flipCoin.buttonPressed;
        }

        public bool PickUpItem()
        {
            return true;
        }
       
    }
}