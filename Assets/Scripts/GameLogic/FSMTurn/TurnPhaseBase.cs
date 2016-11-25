namespace Assets.Scripts.GameLogic.FSMTurn
{
    public abstract class TurnPhaseBase
    {
        protected bool Done;
        protected TurnManager Manager;

        protected TurnPhaseBase(TurnManager manager)
        {
            Manager = manager;
        }

        public abstract void Update();

        public abstract void Start();

        public abstract void End();

        public bool IsDone()
        {
            return Done;
        }
    }
}