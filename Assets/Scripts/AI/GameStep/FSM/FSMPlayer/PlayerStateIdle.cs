using Assets.Scripts.AI.GameStep.FSM.Agents;

namespace Assets.Scripts.AI.GameStep.FSM.FSMPlayer
{
    public class PlayerStateIdle : PlayerStateBase
    {
        public PlayerStateIdle(PlayerAgent agent) : base(agent){}

        public override void Update()
        {

        }

        public override void BeginState()
        {
            Agent.SetWalkAnimation(false);
        }

        public override void EndState()
        {

        }
    }
}