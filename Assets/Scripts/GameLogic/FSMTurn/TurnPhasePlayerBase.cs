using Assets.Scripts.AI;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public abstract class TurnPhasePlayerBase : TurnPhaseBase
    {
        public PlayerActor Player;

        public TurnPhasePlayerBase(TurnManager manager, PlayerActor player) : base(manager)
        {
            Player = player;
        }
    }
}