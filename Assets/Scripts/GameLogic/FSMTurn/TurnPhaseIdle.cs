using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.NodeGrid.Occupants.Specifics;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhaseIdle : TurnPhaseBase
    {
        private bool _exitingIdle;
        private int _enemyRadius;

        public TurnPhaseIdle(TurnManager manager, int enemyRadius) : base(manager)
        {
            _enemyRadius = enemyRadius;
        }

        public override void Update()
        {
            Debug.Log("Idling!");

            _exitingIdle = true;

            if (_exitingIdle)
            {
                Manager.ChangePhase(typeof(TurnPhasePlayerSelection));
            }
        }

        public override void Start()
        {
            _exitingIdle = false;
            BreadthFirst enemyScan = new BreadthFirst();
            enemyScan.Search(Manager.GetPlayerAgent().CurrentNode, 6); //Scan for enemies in given range
            if (enemyScan.Done && enemyScan.Nodes != null)
            {
                HashSet<EnemyAgent> currentEnemies = Manager.GetEnemyHashSet();

                foreach (var node in enemyScan.Nodes)
                {
                    if (node.HasEnemy)
                    {
                        EnemyAgent enemy = (node.Occupant as EnemyAgent);
                        if (enemy != null)
                        {
                            if (!currentEnemies.Contains(enemy))
                            {
                                Debug.Log("Enemy " + enemy.AgentName + " has been successfully added!");
                                Manager.AddEnemy(enemy);
                            }
                            else
                            {
                                Debug.Log("Enemy " + enemy.AgentName + " already was an enemy!");
                            }
                        }
                        else
                        {
                            Debug.Log("Something went wrong while adding the enemy to the gamephase");
                        }
                    }
                }
            }
        }

        public void SetEnemyRadarRadius(int radius)
        {
            _enemyRadius = radius;
        }

        public override void End()
        {
            Debug.ClearDeveloperConsole();
            Manager.SetFirstEnemy();
        }
    }
}