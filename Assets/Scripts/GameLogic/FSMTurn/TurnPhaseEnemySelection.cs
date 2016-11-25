using System.Collections.Generic;
using Assets.Scripts.AI;
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
            //Not Needed here, enemy can make selection and nextphase in 1 go in the start block
        }

        public override void Start()
        {
            Enemy = Manager.GetCurrentEnemy();
            //Enemy = Manager.RefreshCurrentEnemy();
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
        }

        public override void End()
        {
            //Not Needed
        }
    }
}