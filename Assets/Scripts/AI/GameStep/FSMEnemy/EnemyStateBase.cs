namespace Assets.Scripts.AI.GameStep.FSMEnemy
{
    public abstract class EnemyStateBase
    {
        protected EnemyAgent Agent;

        protected EnemyStateBase(EnemyAgent agent)
        {

        }

        public abstract void Update();

        public abstract void BeginState();

        public abstract void EndState();
    }
}