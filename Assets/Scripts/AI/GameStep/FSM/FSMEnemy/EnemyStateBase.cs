using Assets.Scripts.AI.GameStep.FSM.Agents;

namespace Assets.Scripts.AI.GameStep.FSM.FSMEnemy
{
    public abstract class EnemyStateBase
    {
        protected EnemyAgent Agent;

        protected EnemyStateBase(EnemyAgent agent)
        {
            Agent = agent;
        }

        public abstract void Update();

        public abstract void BeginState();

        public abstract void EndState();
    }
}