using System;
using System.Collections.Generic;
using Assets.Scripts.AI;
using Assets.Scripts.AI.GameStep.FSMEnemy;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnManager : MonoBehaviour
    {
        public  GameObject                      Player;
        public  GameObject                      Grid;
        private PlayerAgent                     _player;
        private List<EnemyAgent>                _enemies;
        private Dictionary<Type, TurnPhaseBase> _phases;
        private TurnPhaseBase                   _currentPhase;
        private EnemyAgent                      _currentEnemy;
        private int                             _currentEnemyIterator = 0;

        void Start()
        {
            if (Player != null) _player = Player.GetComponent<PlayerAgent>();

            _enemies = new List<EnemyAgent>();

            _phases = new Dictionary<Type, TurnPhaseBase>();
            _phases.Add(typeof(TurnPhaseIdle),            new TurnPhaseIdle           (this)               );
            _phases.Add(typeof(TurnPhasePlayerSelection), new TurnPhasePlayerSelection(this, _player)      );
            _phases.Add(typeof(TurnPhasePlayerAction),    new TurnPhasePlayerAction   (this, _player)      );
            _phases.Add(typeof(TurnPhaseEnemySelection),  new TurnPhaseEnemySelection (this, _currentEnemy));
            _phases.Add(typeof(TurnPhaseEnemyAction),     new TurnPhaseEnemyAction    (this, _currentEnemy));

            SetDebugActors();

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


        void SetDebugActors()
        {
            _player = new PlayerAgent();
            _enemies.Add(new EnemyAgent());
            _enemies.Add(new EnemyAgent());
            _enemies.Add(new EnemyAgent());
        }

        public void AddEnemy(EnemyAgent enemy)
        {
            _enemies.Add(enemy);
        }

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
    }
}