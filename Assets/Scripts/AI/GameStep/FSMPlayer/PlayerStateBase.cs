using UnityEngine;

namespace Assets.Scripts.AI
{
    public abstract class PlayerStateBase
    {
        protected PlayerAgent Agent;

        protected PlayerStateBase(PlayerAgent agent)
        {

        }


        public abstract void Update();

        public abstract void BeginState();

        public abstract void EndState();
    }
}