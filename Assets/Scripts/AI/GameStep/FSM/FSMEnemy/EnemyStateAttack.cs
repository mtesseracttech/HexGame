using Assets.Scripts.AI.GameStep.FSM.Agents;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.FSMEnemy
{
    public class EnemyStateAttack : EnemyStateBase
    {
        public EnemyStateAttack(EnemyAgent agent) : base(agent)
        {
            Agent = agent;
        }

        public override void Update()
        {
            Debug.Log("Attacking Player!!!");
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Done attacking");
                Agent.SetState(typeof(EnemyStateIdle));
            }
        }

        public override void BeginState()
        {

        }

        public override void EndState()
        {

        }
    }
}