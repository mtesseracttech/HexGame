using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.GameStep.FSM.FSMPlayer;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhasePlayerAction : TurnPhasePlayerBase
    {
        /// <summary>
        /// In this state, the player executes his previously assigned input
        /// </summary>

        public TurnPhasePlayerAction(TurnManager manager, PlayerAgent player) : base(manager, player) {}

        public override void Update()
        {
            if (Player.IsIdling())
            {
                //if the player has to walk, it will do so until that is nullified and idling again
                //The state itself is responsible for appropriate nulling!!!
                if (Player.WalkPath != null)
                {
                    Player.SetState(typeof(PlayerStateWalking));
                }
                //if the player has to do some other behavior, it will do so until nullified and idling again
                //The state itself is responsible for appropriate nulling!!!
                else if (Player.UpcomingInteractionState != null)
                {
                    Player.SetState(Player.UpcomingInteractionState);
                }
                //when the player finished the above two, it is allowed to go to the first enemy state
                else if (Player.WalkPath == null && Player.UpcomingInteractionState == null)
                {
                    Manager.ChangePhase(typeof(TurnPhaseEnemySelection));
                    //Manager.ChangePhase(typeof(TurnPhasePlayerSelection));
                }
            }
        }

        public override void Start()
        {
            Player.SetState(typeof(PlayerStateIdle));
        }

        public override void End()
        {
        }
    }
}