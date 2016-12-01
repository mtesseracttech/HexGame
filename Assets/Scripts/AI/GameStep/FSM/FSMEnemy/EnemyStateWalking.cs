﻿using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.GameStep.FSM.FSMPlayer;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.FSMEnemy
{
    public class EnemyStateWalking : EnemyStateBase
    {
        private float _movementSpeed = 0.5f;
        private HexNode _targetNode;

        public EnemyStateWalking(EnemyAgent agent) : base(agent) {}

        public override void Update()
        {
            DebugHelpers.DebugList(Agent.WalkPath, "Walk Path: ");

            if (Vector3.Distance(Agent.Position, _targetNode.Position) > _movementSpeed)
            {
                Debug.Log("Moving!");
                Agent.Position -= (Agent.Position - _targetNode.Position).normalized * _movementSpeed;
            }
            else
            {
                Debug.Log("Done Moving!");
                Agent.CurrentNode = _targetNode;
                Agent.SetState(typeof(EnemyStateIdle));
            }
        }

        public override void BeginState()
        {
            _targetNode = Agent.WalkPath[0];
        }

        public override void EndState()
        {
            Agent.WalkPath = null;
        }
    }
}