using System.Collections.Generic;
using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.GameStep.FSM.FSMEnemy;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhaseEnemySelection : TurnPhaseEnemyBase
    {
        private PlayerAgent _player;
        private bool _finishedSelection;

        public TurnPhaseEnemySelection(TurnManager manager, EnemyAgent enemy) : base(manager, enemy)
        {
            _player = Manager.GetPlayerAgent();
        }

        public override void Start()
        {
            _finishedSelection                 = false;

            Enemy                          = Manager.GetCurrentEnemy();
            if (Enemy.IsDead()) Manager.ChangePhase(typeof(TurnPhaseEnemyChange));
            Enemy.WalkPath                 = null;
            Enemy.InteractionTarget        = null;
            Enemy.UpcomingInteractionState = null;
            Enemy.SetState(typeof(EnemyStateIdle)); //Just a little force to make sure the enemy is in the correct state
        }

        public override void Update()
        {
            if (!_finishedSelection)
            {
                HexNode playerNode = _player.GetCurrentNode();
                HexNode enemyNode  = Enemy.GetCurrentNode();

                Manager.Pathfinder.Search(enemyNode, playerNode);
                List<HexNode> path = Manager.Pathfinder.Path;

                if (path != null)
                {
                    SelectInteraction(path, playerNode);
                }
                else
                {
                    Debug.Log("no valid path was found! for " + Enemy.name);
                    _finishedSelection = true;
                }

            }
            else
            {
                Manager.ChangePhase(typeof(TurnPhaseEnemyAction));
            }
        }

        private void SelectInteraction(List<HexNode> path, HexNode playerNode)
        {
            if (path.Count > 2)
            {
                //Can only walk towards player
                Enemy.WalkPath                 = new List<HexNode>{path[0]};
                Enemy.InteractionTarget        = null;
                Enemy.UpcomingInteractionState = null;
            }
            else if (path.Count == 2)
            {
                //Will move towards player and then attack
                Enemy.WalkPath                 = new List<HexNode>{path[0]};
                Enemy.InteractionTarget        = _player.GetCurrentNode();
                Enemy.UpcomingInteractionState = typeof(EnemyStateInteractionPlayer);
            }
            else if (path.Count == 1)
            {
                //Will directly attack player
                Enemy.WalkPath                 = null;
                Enemy.InteractionTarget        = _player.GetCurrentNode();
                Enemy.UpcomingInteractionState = typeof(EnemyStateInteractionPlayer);
            }
            else
            {
                Enemy.WalkPath                 = null;
                Enemy.InteractionTarget        = null;
                Enemy.UpcomingInteractionState = null;
                Debug.Log("Something went wrong during pathfinding for " + Enemy.name + ".\n" +
                          "Nullifying everything and not doing anything next phase");
                //Something went seriously wrong, since we have a negative path or we are on the same node.
            }
            _finishedSelection = true;
        }

        public override void End()
        {

        }
    }
}