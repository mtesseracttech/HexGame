using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.GameStep.FSM.FSMEnemy;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhaseEnemyAction : TurnPhaseEnemyBase
    {
        public TurnPhaseEnemyAction(TurnManager manager, EnemyAgent enemy) : base(manager, enemy)
        {
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Enemy = Manager.GetCurrentEnemy();
                if (Manager.HasNextEnemy())
                {
                    Debug.Log("The current enemy is: " + Enemy.Name);
                    Manager.SetNextEnemy();
                    {
                        Enemy = Manager.GetCurrentEnemy();
                        Debug.Log("Refreshed to enemy" + Enemy.Name);
                        //Manager.ChangePhase(typeof(TurnPhaseEnemySelection));
                    }
                }
                else
                {
                    Debug.Log("The current enemy is: " + Enemy.Name);
                    Manager.SetFirstEnemy();
                    Enemy = Manager.GetCurrentEnemy();
                    Debug.Log("Reset to enemy" + Enemy.Name);
                    //Manager.ChangePhase(typeof(TurnPhaseIdle));
                }


                /*
                if (Enemy.IsIdling())
                {

                    //First Walking is done till the player is idling again if data is present
                    if (Enemy.WalkPath != null)
                    {
                        //if (Enemy.WalkPath.Count > 0)
                        //{
                        //    Enemy.TargetNode = Enemy.WalkPath[0]; //Just a single step is allowed!
                        //}
                        //else Enemy.TargetNode = Enemy.CurrentNode;
                        Enemy.WalkPath  = null;
                        //Enemy.SetState(typeof(EnemyStateStepMovement));
                    }
                    //Then Attacking is executed till idling again if the data is present
                    else if (Enemy.InteractionTarget != null)
                    {
                        //Enemy.InteractionTarget  = Enemy.InteractionTarget;
                        Enemy.InteractionTarget = null;
                        //Enemy.SetState(typeof(EnemyStateAttack));
                    }
                    //If both datas are set to null and the player is idling again, the next state is loaded
                    else if (Enemy.WalkPath == null && Enemy.InteractionTarget == null)
                    {
                        if (Manager.HasNextEnemy())
                        {
                            Manager.SetNextEnemy();
                            Debug.Log("Ending Current Enemy Phase, Changed to next one!");
                            Manager.ChangePhase(typeof(TurnPhaseEnemySelection));
                        }
                        else
                        {
                            Debug.Log("No more enemies to do. End of the step!");
                            Manager.ChangePhase(typeof(TurnPhaseIdle));
                        }
                    }

                }
                */
            }
        }

        public override void Start()
        {
            Enemy = Manager.GetCurrentEnemy();
            Done = false;
        }

        public override void End()
        {
        }
    }
}