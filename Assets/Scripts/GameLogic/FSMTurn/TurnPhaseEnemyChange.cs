namespace Assets.Scripts.GameLogic.FSMTurn
{
    class TurnPhaseEnemyChange : TurnPhaseBase
    {
        public TurnPhaseEnemyChange(TurnManager manager) : base(manager)
        {
        }

        public override void Update()
        {
        }
                
        public override void Start()
        {
            if (Manager.HasNextEnemy())
            {
                Manager.SetNextEnemy();
                Manager.ChangePhase(typeof(TurnPhaseEnemySelection));
            }
            else
            {
                Manager.ChangePhase(typeof(TurnPhaseIdle));
            }
        }

        public override void End()
        {
        }
    }
}
