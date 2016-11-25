using Assets.Scripts.AI.GameStep.FSM.Agents;

namespace Assets.Scripts.AI.GameStep.FSM.FSMPlayer
{
    public abstract class PlayerStateBase
    {
        protected PlayerAgent Agent;

        protected PlayerStateBase(PlayerAgent agent)
        {

        }

        public abstract void Update();

        public abstract void BeginState();

        public abstract void EndState();
    }
}