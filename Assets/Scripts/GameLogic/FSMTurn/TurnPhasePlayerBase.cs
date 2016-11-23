using Assets.Scripts.AI.GameStep.FSM.Agents;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public abstract class TurnPhasePlayerBase : TurnPhaseBase
    {
        public PlayerAgent Player;

        protected TurnPhasePlayerBase(TurnManager manager, PlayerAgent player) : base(manager)
        {
            Player = player;
        }
    }
}