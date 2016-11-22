using Assets.Scripts.AI.GameStep.FSMEnemy;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public abstract class TurnPhaseEnemyBase : TurnPhaseBase
    {
        protected EnemyActor Enemy;

        public TurnPhaseEnemyBase(TurnManager manager, EnemyActor enemy) : base(manager)
        {
            Enemy = enemy;
        }
    }
}