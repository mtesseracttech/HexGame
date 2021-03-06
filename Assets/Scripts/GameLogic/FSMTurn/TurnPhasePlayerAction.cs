﻿using System;
using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.AI.GameStep.FSM.FSMPlayer;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnPhasePlayerAction : TurnPhasePlayerBase
    {
        /// <summary>
        /// In this state, the player executes his previously assigned input
        /// </summary>

        private     bool     _tileCheckDone;
        private     Random   _rand;

        public TurnPhasePlayerAction(TurnManager manager, PlayerAgent player) : base(manager, player)
        {
            _rand = new Random();
        }

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
                //this does a check to see if the player has stepped on something
                else if (!_tileCheckDone)
                {
                    RadiationTile rTile = Manager.OnRadiationTile(Player.CurrentNode);
                    if (rTile != null)
                    {
                        rTile.ShowParticles();
                        int radDamage = _rand.Next(1, 10);
                        Debug.Log(Player.CoinFlip.PlayerStats.MaximumHealth);
                        Player.CoinFlip.PlayerStats.Radiation = radDamage;
                        Debug.Log("Radiation Damaged Max Health for " + radDamage + " points.");
                        Debug.Log(Player.CoinFlip.PlayerStats.MaximumHealth);
                    }
                    _tileCheckDone = true;
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
            _tileCheckDone = false;
        }

        public override void End()
        {
        }
    }
}