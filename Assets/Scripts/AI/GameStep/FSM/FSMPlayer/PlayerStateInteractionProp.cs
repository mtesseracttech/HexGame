﻿using Assets.Scripts.AI.GameStep.FSM.Agents;

namespace Assets.Scripts.AI.GameStep.FSM.FSMPlayer
{
    public class PlayerStateInteractionProp : PlayerStateBase
    {
        public PlayerStateInteractionProp(PlayerAgent agent) : base(agent)
        {
        }

        public override void Update()
        {

        }

        public override void BeginState()
        {

        }

        public override void EndState()
        {
            Agent.InteractionTarget        = null;
            Agent.UpcomingInteractionState = null;
        }
    }
}