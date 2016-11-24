using Assets.Scripts.AI;
using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.NPC;

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
                if (Manager.WalkPath != null)
                {
                    if (Manager.WalkPath.Count > 0)
                    {
                        Player.TargetNode = Manager.WalkPath[0]; //Just a single step is allowed!
                    }
                    else Player.TargetNode = Player.CurrentNode;
                    Manager.WalkPath  = null;
                    Player.SetState(typeof(PlayerStateStepMovement));
                }
                //Then Attacking is executed till idling again if the data is present
                else if (Manager.AttackTarget != null)
                {
                    Player.AttackTarget  = Manager.AttackTarget;
                    Manager.AttackTarget = null;
                    Player.SetState(typeof(PlayerStateAttack));
                }
                //If both datas are set to null and the player is idling again, the next state is loaded
                else if (Manager.WalkPath == null && Manager.AttackTarget == null)
                {
                    Manager.ChangePhase(typeof(TurnPhaseEnemySelection));
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