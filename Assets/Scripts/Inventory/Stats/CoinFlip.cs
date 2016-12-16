using System.Collections;
using Assets.Scripts.Inventory.Stats.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory.Stats
{
    public class CoinFlip : MonoBehaviour {

        public GameObject attackButton;

        [Header("HUD of player")]
        [SerializeField]
        private Text _attackText;
        [SerializeField]
        private Text _defenceText;
        [SerializeField]
        private Text _healthText;
        [SerializeField]
        private Text _coins;
        [SerializeField]
        private Text _damageDone;

        public int AttackCoinsPlayer;
        public int DefenceCoinsPlayer;

        private int _defAttackAmountPlayer;  //defined attack stats
        private int _defDefenseAmountPlayer; //defined defense stats
        private int _defCoinAmountPlayer;    //defined coin amount

        public Player.PlayerStats PLayerStats;
        public bool buttonPressed;

        [Header("HUD of enemy")]
        [SerializeField]
        private Text _attackTextEnemy;
        [SerializeField]
        private Text _defenceTextEnemy;
        [SerializeField]
        private Text _healthTextEnemy;
        [SerializeField]
        private Text _coinsEnemy;
        [SerializeField]
        private Text _damageDoneEnemy;

        public int AttackCoinsEnemy;
        public int DefenceCoinsEnemy;

        private int _defAttackAmountEnemy;
        private int _defDefenseAmountEnemy;
        private int _defCoinAmountEnemy;

       
        public EnemyStats ENemyStats;

        private void Start()
        {
            /**/
            RefreshStats();

            _attackText.text = "Attack: " + PLayerStats.AttackStats;
            _defenceText.text = "Defence: " + PLayerStats.DefenseStats;

            _attackTextEnemy.text = "Attack: " + ENemyStats.AttackStats;
            _defenceTextEnemy.text = "Defence: " + ENemyStats.DefenseStats;

            //defining player stats for reseting
            _defAttackAmountPlayer = PLayerStats.AttackStats;
            _defDefenseAmountPlayer = PLayerStats.DefenseStats;
            _defCoinAmountPlayer = PLayerStats.CoinHave;

            //defining player stats for reseting
            _defAttackAmountEnemy = ENemyStats.AttackStats;
            _defDefenseAmountEnemy = ENemyStats.DefenseStats;
            _defCoinAmountEnemy = ENemyStats.CoinHave;
            /**/
        }


        public void FlipCoins()
        {
            ResetValues();

           // CheckItemsForValues();
            
            attackButton.SetActive(false);
            //--------------------------------------------------------------------//
            //              Player FLIP                                           //
            for (int i = 0; i < PLayerStats.CoinHave; i++)
            {
                int value = (int)Mathf.Round(Random.Range(1, 2) * Random.value);

                switch (value)
                {
                    case 0:
                        AttackCoinsPlayer++;
                        break;
                    case 1:
                        DefenceCoinsPlayer++;
                        break;
                    default:
                        break;
                }
            }

            _attackText.text = "Attack: " + PLayerStats.AttackStats + "+" + AttackCoinsPlayer; // + " +" + _bonusAttackCoins;
            _defenceText.text = "Defence: " + PLayerStats.DefenseStats + "+" + DefenceCoinsPlayer; // + " +" + _bonusDefenceCoins;

            PLayerStats.AttackStats += AttackCoinsPlayer;
            PLayerStats.DefenseStats += DefenceCoinsPlayer;
            
            
           // Debug.Log("Player Attack coin: " + AttackCoinsPlayer + "Player Defence coin: " + DefenceCoinsPlayer);


            //--------------------------------------------------------------------//
            //             ENEMY flip                                            //
            for (int i = 0; i < ENemyStats.CoinHave; i++)
            {
                int value = (int)Mathf.Round(Random.Range(1, 2) * Random.value);

                switch (value)
                {
                    case 0:
                        AttackCoinsEnemy++;
                        break;
                    case 1:
                        DefenceCoinsEnemy++;
                        break;
                    default:
                        break;
                }
            }
            _attackTextEnemy.text = "Attack: " + ENemyStats.AttackStats + "+" + AttackCoinsEnemy; // + " +" + _bonusAttackCoins;
            _defenceTextEnemy.text = "Defence: " + ENemyStats.DefenseStats + "+" + DefenceCoinsEnemy; // + " +" + _bonusDefenceCoins;

            ENemyStats.AttackStats += AttackCoinsEnemy;
            ENemyStats.DefenseStats += DefenceCoinsEnemy;

            RefreshStats();
            //do damge


            StartCoroutine(DoDamage());
            RefreshStats();
            

        }

        IEnumerator DoDamage()
        { 
             //do damage to player
            PLayerStats.DoDamageCombat(ENemyStats.AttackStats, PLayerStats.DefenseStats);

            //do damage to enemy
            ENemyStats.DoDamageCombat(PLayerStats.AttackStats, ENemyStats.DefenseStats);
            RefreshStats();

            yield return new WaitForSeconds(2f);
            buttonPressed = true;

        }

        void RefreshStats()
        {
            buttonPressed = false;
            //-----------------------------------------------------------//
            //                          PLAYER                          //
            _healthText.text = "Health: " + PLayerStats.CurrentHealth;
            _coins.text = "Coins: " + PLayerStats.CoinHave;
            _damageDone.text = "DamageDone: " + PLayerStats.DamageIsDone;

            //-----------------------------------------------------------//
            //                         ENEMY                            //
            _healthTextEnemy.text = "Health: " + ENemyStats.CurrentHealth;
            _coinsEnemy.text = "Coins: " + ENemyStats.CoinHave;
            _damageDoneEnemy.text = "DamageDone: " + ENemyStats.DamageIsDone;
        }

        void ResetValues()
        {
            //-----------------------------------------------------------//
            //                          PLAYER                          //
            PLayerStats.CoinHave = _defCoinAmountPlayer;
            PLayerStats.AttackStats = _defAttackAmountPlayer;
            PLayerStats.DefenseStats = _defDefenseAmountPlayer;
            AttackCoinsPlayer = 0;
            DefenceCoinsPlayer = 0;

            //-----------------------------------------------------------//
            //                         ENEMY                            //
            ENemyStats.CoinHave = _defCoinAmountEnemy;
            ENemyStats.AttackStats = _defAttackAmountEnemy;
            ENemyStats.DefenseStats = _defDefenseAmountEnemy;
            AttackCoinsEnemy = 0;
            DefenceCoinsEnemy = 0;
        }

    }
}
