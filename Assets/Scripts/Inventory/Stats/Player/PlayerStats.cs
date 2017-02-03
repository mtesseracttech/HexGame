using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory.Stats.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField]
        private int _maximumHealth;
        [SerializeField]
        private int _currentHealth;
        [SerializeField]
        private int _attackStats;
        [SerializeField]
        private int _defenseStats;
        [SerializeField]
        private int _coinHave;
        [SerializeField]
        private int _damageIsDone;
        [SerializeField]
        private int _radiation;

        public Image HealthBar;

        private void Start()
        {
            _maximumHealth = _currentHealth;
        }

        void Update()
        {
            GameOver();
            ReduceMaxHealth();
            SetMaxHealth();
        }


        public int CurrentHealth
        {
            set { _currentHealth = value; }
            get { return _currentHealth; }
        }
        public int AttackStats
        {
            set { _attackStats = value; }
            get { return _attackStats; }
        }
        public int DefenseStats
        {
            set { _defenseStats = value; }
            get { return _defenseStats; }
        }
        public int CoinHave
        {
            set { _coinHave = value; }
            get { return _coinHave; }
        }
        public int DamageIsDone
        {
            set { _damageIsDone = value; }
            get { return _damageIsDone; }
        }
        public int Radiation
        {
            set { _radiation = value; }
            get { return _radiation; }
        }
        public int MaximumHealth
        {
            set { _maximumHealth = value; }
            get { return _maximumHealth; }
        }

        private void SetMaxHealth()
        {
            if (_currentHealth > _maximumHealth)
            {
                //_maximumHealth = _currentHealth;
                _currentHealth = _maximumHealth;
            }
        }

        private void ReduceMaxHealth()
        {
            if (_radiation > 0)
            {
                _maximumHealth = _maximumHealth - _radiation;
                _radiation = 0;
            }
        }

        public void DoDamageCombat(int attack, int defense)
        {
            if (attack > defense)
            {
                Debug.Log("damage done to player");
                _damageIsDone = Mathf.Abs(defense - attack);
                _currentHealth -= _damageIsDone;
                HealthBar.fillAmount = CurrentHealth / MaximumHealth;
            }
        
        }

        void GameOver()
        {
            if (_currentHealth <= 0)
            {
                SceneManager.LoadScene(4);
            }
        }
    }
}
