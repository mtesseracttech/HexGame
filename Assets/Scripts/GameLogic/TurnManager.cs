using System;
using System.Collections.Generic;
using Assets.Scripts.AI;
using Assets.Scripts.AI.GameStep.FSMEnemy;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FSMTurn
{
    public class TurnManager : MonoBehaviour
    {
        public GameObject Player;
        public GameObject Grid;

        private PlayerActor _player;
        private List<EnemyActor> _enemies;

        private Dictionary<Type, TurnPhaseBase> _phases;
        private TurnPhaseBase _currentPhase;
        private EnemyActor _currentEnemy;

        void Start()
        {
            _phases = new Dictionary<Type, TurnPhaseBase>();
            _phases.Add(typeof(TurnPhaseIdle),            new TurnPhaseIdle           (this)               );
            _phases.Add(typeof(TurnPhasePlayerSelection), new TurnPhasePlayerSelection(this, _player)      );
            _phases.Add(typeof(TurnPhasePlayerAction),    new TurnPhasePlayerAction   (this, _player)      );
            _phases.Add(typeof(TurnPhaseEnemySelection),  new TurnPhaseEnemySelection (this, _currentEnemy));
            _phases.Add(typeof(TurnPhaseEnemyAction),     new TurnPhaseEnemyAction    (this, _currentEnemy));
            _currentPhase = _phases[typeof(TurnPhaseIdle)];
        }

        public void ChangePhase(Type newPhase)
        {
            Debug.Log("Changing from phase: " + _currentPhase.GetType() + " to: " + newPhase);
            _currentPhase.End();
            _currentPhase = _phases[newPhase];
            _currentPhase.Start();
        }
    }
}