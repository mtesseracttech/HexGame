using Assets.Scripts.AI.GameStep.FSMEnemy;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public abstract class TurnPhaseEnemyBase : TurnPhaseBase
    {
        protected EnemyAgent Enemy;

        public TurnPhaseEnemyBase(TurnManager manager, EnemyAgent enemy) : base(manager)
        {
            Enemy = enemy;
        }
    }
}