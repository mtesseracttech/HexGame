namespace Assets.Scripts.AI.GameStep.FSMEnemy
{
    public abstract class EnemyStateBase
    {
        protected EnemyActor Actor;

        protected EnemyStateBase(EnemyActor actor)
        {

        }

        public abstract void Update();

        public abstract void BeginState();

        public abstract void EndState();
    }
}