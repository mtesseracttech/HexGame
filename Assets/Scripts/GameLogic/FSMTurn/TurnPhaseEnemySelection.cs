using System.Collections.Generic;
using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhaseEnemySelection : TurnPhaseEnemyBase
    {
        private PlayerAgent _player;

        public TurnPhaseEnemySelection(TurnManager manager, EnemyAgent enemy) : base(manager, enemy)
        {
            _player = Manager.GetPlayerAgent();
        }

        public override void Update()
        {
            Enemy.WalkPath     = null;
            Enemy.AttackTarget = null;

            HexNode playerLocation = _player.GetCurrentNode();
            HexNode enemyLocation  =  Enemy .GetCurrentNode();

            Pathfinder pathfinder = new Pathfinder();

            pathfinder.Search(enemyLocation, playerLocation);
            List<HexNode> path = pathfinder.Path;

            if (path != null)
            {
                if (path.Count <= 2 && playerLocation.HasPlayer)
                {
                    Debug.Log("Node contains player, moving to closest tile");
                    if (path.Count == 2)
                    {
                        List<HexNode> tempPath = new List<HexNode> {path[0]};
                        Enemy.WalkPath = tempPath;
                    }
                    Enemy.AttackTarget = playerLocation;
                    Manager.ChangePhase(typeof(TurnPhaseEnemyAction));

                }
                else if (path.Count == 1)
                {
                    Debug.Log("Moving to closer tile");
                    List<HexNode> tempPath = new List<HexNode>();
                    tempPath.Add(path[0]);
                    Enemy.WalkPath = tempPath;
                    Enemy.AttackTarget = null;
                    Manager.ChangePhase(typeof(TurnPhaseEnemyAction));
                }
                else
                {
                    //Debug.Log(path.Count);
                    Debug.Log("The player is too far away to walk to/attack!");
                }
            }
            else
            {
                Debug.Log("No possible way found to get to the player");
                Manager.ChangePhase(typeof(TurnPhaseEnemyAction));
            }
        }

        public override void Start()
        {
            Enemy = Manager.GetCurrentEnemy();
        }

        public override void End()
        {
            //Not Needed
        }
    }
}


















/*
            Enemy = Manager.GetCurrentEnemy();
            Enemy.WalkPath = null;
            Enemy.AttackTarget = null;

            HexNode playerNode = _player.CurrentNode;
            HexNode ownNode    = Enemy.CurrentNode;

            Pathfinder pathfinder = new Pathfinder();
            pathfinder.Search(ownNode, playerNode);
            List<HexNode> path = pathfinder.Path;
            if (path != null)
            {
                string enemySelectionInfo = "EnemySelection Selection Info\n" +
                                            "Found Path Length: " + path.Count + "\n";
                if (path.Count <= 2)
                {
                    if (path.Count == 2) //Walk + attack
                    {
                        enemySelectionInfo += "So Walk + Attack is needed!";
                        List<HexNode> tempPath = new List<HexNode> {path[0]};
                        Enemy.WalkPath = tempPath;
                    }
                    //Direct Attack!
                    Enemy.AttackTarget = _player.CurrentNode;
                }
                else
                {
                    //Move towards
                    enemySelectionInfo += "So player is too far away and I can only walk there!";
                    List<HexNode> tempPath = new List<HexNode> {path[0]};
                    Enemy.WalkPath = tempPath;
                }
                Debug.Log(enemySelectionInfo);
            }
            else
            {
                Debug.Log("Could not find any way to get to the player!");
            }

            Manager.ChangePhase(typeof(TurnPhaseEnemyAction));
            */