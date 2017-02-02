using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.AI.GameStep.FSM.FSMEnemy
{
    public class EnemyStateInteractionPlayer : EnemyStateBase
    {
        private HexNode     _playerNode;
        private PlayerAgent _player;

        public EnemyStateInteractionPlayer(EnemyAgent agent) : base(agent) {}

        public override void Update()
        {
            Debug.Log(Agent.AgentName + " is attacking " + _player.name);
            Agent.CombatUi.SetActive(true);
            Agent.CoinFlip.ENemyStats = Agent._EnemyStats;
            if (Agent.CoinFlip.AttackEnds)
            {
                
                Agent.SetState(typeof(EnemyStateIdle));
                Agent.CoinFlip.AttackEnds = false;
                Agent.CoinFlip.AttackButton = false;
            }
        }

        public override void BeginState()
        {
            Agent.CoinFlip._enemyImage.sprite = Agent.EnemyBattleImage;
            _playerNode = Agent.InteractionTarget;
            _player     = Agent.InteractionTarget.Occupant as PlayerAgent;
        }

        public override void EndState()
        {
            Agent.CombatUi.SetActive(false);
            Agent.InteractionTarget        = null;
            Agent.UpcomingInteractionState = null;
            Agent.CoinFlip.AttackEnds = false;
            Agent.CoinFlip.AttackButton = false;
        }
    }
}