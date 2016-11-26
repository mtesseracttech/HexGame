using Assets.Scripts.AI;
using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.GameStep.FSM.FSMPlayer;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhasePlayerAction : TurnPhasePlayerBase
    {
        public TurnPhasePlayerAction(TurnManager manager, PlayerAgent player) : base(manager, player)
        {
        }

        public override void Update()
        {
            if (Player.IsIdling())
            {
                //First Walking is done till the player is idling again if data is present
                if (Player.WalkPath != null)
                {
                    if (Player.WalkPath.Count > 0)
                    {
                        Player.TargetNode = Player.WalkPath[0]; //Just a single step is allowed!
                    }
                    else Player.TargetNode = Player.CurrentNode;
                    Player.WalkPath  = null;
                    Player.SetState(typeof(PlayerStateStepMovement));
                }
                //Then Attacking is executed till idling again if the data is present
                else if (Player.AttackTarget != null)
                {
                    Player.AttackTarget  = Player.AttackTarget;
                    Player.AttackTarget = null;
                    Player.SetState(typeof(PlayerStateAttack));
                }
                //If both datas are set to null and the player is idling again, the next state is loaded
                else if (Player.WalkPath == null && Player.AttackTarget == null)
                {
                    Manager.ChangePhase(typeof(TurnPhaseEnemySelection));
                    //Manager.ChangePhase(typeof(TurnPhaseIdle));
                }
            }
        }

        public override void Start()
        {
            Player.SetState(typeof(PlayerStateIdle));
        }

        public override void End()
        {
            Player.AttackTarget = null;
            Player.TargetNode   = null;
            Player.SetState(typeof(PlayerStateIdle));
        }
    }
}