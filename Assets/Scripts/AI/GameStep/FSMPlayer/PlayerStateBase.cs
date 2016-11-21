using UnityEngine;

namespace Assets.Scripts.AI
{
    public abstract class PlayerStateBase
    {
        protected PlayerActor Actor;

        protected PlayerStateBase(PlayerActor actor)
        {

        }


        public abstract void Update();

        public abstract void BeginState();

        public abstract void EndState();
    }
}