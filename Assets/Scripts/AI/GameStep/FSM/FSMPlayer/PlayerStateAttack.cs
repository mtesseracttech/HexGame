using Assets.Scripts.AI.GameStep.FSM.Agents;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class PlayerStateAttack : PlayerStateBase
    {
        public PlayerStateAttack(PlayerAgent agent) : base(agent)
        {
            Agent = agent;
        }

        public override void Update()
        {
            Debug.Log("Attacking Enemy!!!");
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Done attacking");
                Agent.SetState(typeof(PlayerStateIdle));
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