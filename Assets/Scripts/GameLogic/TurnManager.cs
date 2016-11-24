using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AI;
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
        private HexNode                         _attackTarget;
        private List<HexNode>                   _walkPath;


        void Start()
        {
            if (HexNodeManager != null)
            {
                _hexNodesManager = HexNodeManager.GetComponent<HexNodesManager>();
                if (_hexNodesManager == null)
                {
                    Debug.Log("There was no HexNodesManager script bound to the given HexNodeManager instance!");
                }
                else
                {
                     Debug.Log("Successfully linked the HexNodeManager to the TurnManager!");
                }
            }
            else
            {
                Debug.Log("No HexNodeManager instance was supplied to the TurnManager!");
            }

            if (Player != null)
            {
                _player = Player.GetComponent<PlayerAgent>();
                if (_player == null)
                {
                    Debug.Log("There was no PlayerAgent script bound to the given Player instance!");
                }
                else
                {
                    Debug.Log("Successfully linked the PlayerAgent to the TurnManager!");
                }
            }
            else
            {
                Debug.Log("No Player instance was supplied to the TurnManager!");
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
                        Debug.Log("An enemy is missing an EnemyAgent component!");
                    }
                }
            }
            else
            {
                _enemies = new List<EnemyAgent>();
            }

            //Setting up the cache
            _phases = new Dictionary<Type, TurnPhaseBase>();
            _phases.Add(typeof(TurnPhaseIdle),            new TurnPhaseIdle           (this)               );
            _phases.Add(typeof(TurnPhasePlayerSelection), new TurnPhasePlayerSelection(this, _player)      );
            _phases.Add(typeof(TurnPhasePlayerAction),    new TurnPhasePlayerAction   (this, _player)      );
            _phases.Add(typeof(TurnPhaseEnemySelection),  new TurnPhaseEnemySelection (this, _currentEnemy));
            _phases.Add(typeof(TurnPhaseEnemyAction),     new TurnPhaseEnemyAction    (this, _currentEnemy));


            //Initializing the first state manually
            _currentPhase = _phases[typeof(TurnPhaseIdle)];
            _currentPhase.Start();
        }


        public void ChangePhase(Type newPhase)
        {
            Debug.Log("Changing from phase: " + _currentPhase.GetType() + " to: " + newPhase);
            _currentPhase.End();
            _currentPhase = _phases[newPhase];
            _currentPhase.Start();
        }


        void Update()
        {
            _currentPhase.Update();
        }


        public bool SetNextEnemy(EnemyAgent enemy)
        {
            _currentEnemyIterator += 1;
            if (_currentEnemyIterator >= _enemies.Count || _currentEnemyIterator < 0)
            {
                _currentEnemyIterator = 0;
                return false;
            }
            _currentEnemy = _enemies[_currentEnemyIterator];
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

        public EnemyAgent RefreshCurrentEnemy()
        {
            return _currentEnemy;
        }

        public Type GetPhase()
        {
            return _currentPhase.GetType();
        }

        public HexNode AttackTarget
        {
            get { return  _attackTarget; }
            set { _attackTarget = value; }
        }

        public List<HexNode> WalkPath
        {
            get { return  _walkPath; }
            set { _walkPath = value; }
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
    }
}