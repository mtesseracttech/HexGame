using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.FSMPlayer
{
    public class PlayerStateInteractionProp : PlayerStateBase
    {
        private HexNode       _itemNode;
        private Items         _item;
        private float         _rotationTime             = 0.5f;
        private float         _rotationAccumulator      = 0.0f;
        private float         _movementSpeed            = 0.5f;
        private Quaternion    _targetRotation;

        public PlayerStateInteractionProp(PlayerAgent agent) : base(agent)
        {}

        public override void Update()
        {

            if (_rotationAccumulator < _rotationTime)
            {
                _rotationAccumulator += Time.deltaTime;
            }
            else
            {
                _rotationAccumulator = _rotationTime;
            }


            float rotationFactor = _rotationAccumulator / _rotationTime;
            Agent.Rotation = Quaternion.Slerp(Agent.Rotation, _targetRotation, rotationFactor);


            if (Vector3.Distance(Agent.Position, _itemNode.Position) > _movementSpeed * (Time.deltaTime * 60))
            {
                Agent.Rotation = Quaternion.Slerp(Agent.Rotation, _targetRotation, rotationFactor);
                Agent.Position -= (Agent.Position - _itemNode.Position).normalized * _movementSpeed * (Time.deltaTime * 60);
            }
            else
            {
                Agent.CurrentNode = _itemNode;
                Agent.SetState(typeof(PlayerStateIdle));
            }
        }

        public override void BeginState()
        {
            
            _itemNode = Agent.InteractionTarget;
            _item = Agent.InteractionTarget.Occupant as Items;
            _targetRotation = Quaternion.LookRotation(_itemNode.Position - Agent.Position);
            _rotationAccumulator = 0;


            if (_item != null) _item.Interact();
        }

        public override void EndState()
        {
            Agent.InteractionTarget        = null;
            Agent.UpcomingInteractionState = null;
        }
    }
}