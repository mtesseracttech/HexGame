using System;
using System.Collections.Generic;
using Assets.Scripts.AI.GameStep.FSM.Agents;
using Assets.Scripts.GameLogic.FSMTurn;
using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    public class TurnManager : MonoBehaviour
    {
        public  GameObject                      Player;
        public  GameObject[]                    Enemies;
        public  GameObject                      HexNodeManager;
        private PlayerAgent                     _player;
        private List<EnemyAgent>                _enemies;
        private Dictionary<Type, TurnPhaseBase> _phases;
        private TurnPhaseBase                   _currentPhase;
        private EnemyAgent                      _currentEnemy;
        private int                             _currentEnemyIterator = 0;
        private HexNodesManager                 _hexNodesManager;

        void Start()
        {
            string turnManagerDebug = "TurnManager Start Info:\n";
            if (HexNodeManager != null)
            {
                _hexNodesManager = HexNodeManager.GetComponent<HexNodesManager>();
                if (_hexNodesManager == null)
                {
                    turnManagerDebug += "There was no HexNodesManager script bound to the given HexNodeManager instance!\n";
                }
                else
                {
                    turnManagerDebug += "Successfully linked the HexNodeManager to the TurnManager!\n";
                }
            }
            else
            {
                turnManagerDebug += "No HexNodeManager instance was supplied to the TurnManager!\n";
            }

            if (Player != null)
            {
                _player = Player.GetComponent<PlayerAgent>();
                if (_player == null)
                {
                    turnManagerDebug += "There was no PlayerAgent script bound to the given Player instance!\n";
                }
                else
                {
                    turnManagerDebug += "Successfully linked the PlayerAgent to the TurnManager!\n";
                }
            }
            else
            {
                turnManagerDebug += "No Player instance was supplied to the TurnManager!\n";
            }

            if (Enemies != null && Enemies.Length > 0)
            {
                _enemies = new List<EnemyAgent>();
                foreach (var enemy in Enemies)
                {
                    EnemyAgent tempAgent = enemy.GetComponent<EnemyAgent>();
                    if (tempAgent != null)
                    {
                        _enemies.Add(tempAgent);
                    }
                    else
                    {
                        Debug.Log("An enemy is missing an EnemyAgent component!\n");
                    }
                }
                turnManagerDebug += _enemies.Count + " Enemies were added to the TurnManager\n";
            }
            else
            {
                _enemies = new List<EnemyAgent>();
                turnManagerDebug += "No enemies were added to the TurnManager\n";
            }

            //Setting up the cache
            _phases = new Dictionary<Type, TurnPhaseBase>();
            _phases.Add(typeof(TurnPhaseIdle),            new TurnPhaseIdle           (this)               );
            _phases.Add(typeof(TurnPhasePlayerSelection), new TurnPhasePlayerSelection(this, _player)      );
            _phases.Add(typeof(TurnPhasePlayerAction),    new TurnPhasePlayerAction   (this, _player)      );
            _phases.Add(typeof(TurnPhaseEnemySelection),  new TurnPhaseEnemySelection (this, _currentEnemy));
            _phases.Add(typeof(TurnPhaseEnemyAction),     new TurnPhaseEnemyAction    (this, _currentEnemy));

            turnManagerDebug += "Phase Cache is initialized!";

            Debug.Log(turnManagerDebug);

            //Initializing the first state manually
            _currentPhase = _phases[typeof(TurnPhaseIdle)];
            _currentPhase.Start();
        }


        public void ChangePhase(Type newPhase)
        {
            Debug.Log("Changing from phase: " + _currentPhase.GetType() + " to: " + newPhase);
            if (_currentPhase.GetType() == typeof(TurnPhasePlayerAction) && newPhase == typeof(TurnPhaseEnemySelection))
            {
                if (_enemies.Count == 0)
                {
                    Debug.Log("No enemies available, going back to idle state!");
                    newPhase = typeof(TurnPhaseIdle); //if no enemies are present, switching back to
                }
            }
            _currentPhase.End();
            _currentPhase = _phases[newPhase];
            _currentPhase.Start();
        }


        void Update()
        {
            _currentPhase.Update();
        }

        public void SetNextEnemy()
        {
            _currentEnemyIterator++;
            _currentEnemy = _enemies[_currentEnemyIterator];
        }

        public bool HasNextEnemy()
        {
            if ((_currentEnemyIterator + 1) >= _enemies.Count)
            {
                return false;
            }
            return true;
        }

        public void AddEnemy(EnemyAgent enemy)
        {
            _enemies.Add(enemy);
        }

        // Don't call inside of step cycle!
        // OutOfBounds problems will happen!
        public void RemoveEnemy(EnemyAgent enemy)
        {
            _enemies.Remove(enemy);
        }

        public void SetPlayerStates(Type state)
        {
            _player.SetState(state);
        }

        public void SetEnemyStates(Type state)
        {
            foreach (var enemy in _enemies)
            {
                enemy.SetState(state);
            }
        }

        public PlayerAgent GetPlayerAgent()
        {
            return _player;
        }

        public Type GetPhase()
        {
            return _currentPhase.GetType();
        }

        public EnemyAgent GetCurrentEnemy()
        {
            return _enemies[_currentEnemyIterator];
        }

        public void RemoveDeadEnemies()
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                if (_enemies[i].IsDead())
                {
                    Destroy(_enemies[i].gameObject);
                    _enemies.RemoveAt(i);
                }
            }
        }

        public void SetFirstEnemy()
        {
            if (_enemies.Count > 0)
            {
                _currentEnemyIterator = 0;
                _currentEnemy = _enemies[0];
            }
        }
    }
}