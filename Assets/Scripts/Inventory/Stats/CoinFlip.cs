using System.Collections;
using Assets.Scripts.Inventory.Stats.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory.Stats
{
    public class CoinFlip : MonoBehaviour {

        public GameObject attackButton;
        public Sprite attack;
        public Sprite defense;
        public Image[] coinSlot;
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

        private int _attackCoinsPlayer;
        private int _defenceCoinsPlayer;

        private int _defAttackAmountPlayer;  //defined attack stats
        private int _defDefenseAmountPlayer; //defined defense stats
        private int _defCoinAmountPlayer;    //defined coin amount

        public Player.PlayerStats PLayerStats;
        private bool _buttonPressed;
        [Space]
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

        private int AttackCoinsEnemy;
        private int DefenceCoinsEnemy;

        private int _defAttackAmountEnemy;
        private int _defDefenseAmountEnemy;
        private int _defCoinAmountEnemy;

       
        public EnemyStats ENemyStats;//get enemie that we attack

        private void Start()
        {
            //loop through how many slots we have.
            //then check how many coins we have and then fill the slots with default image
            //when flipped update the slot image with attack or defense
            for (int i = 0; i < coinSlot.Length; i++)
            {
                coinSlot[i].sprite = defense;
            }
        }

        private void Update()
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

            int[] coins = new int[coinSlot.Length];
            int iter = 0;


            for (int i = 0; i < PLayerStats.CoinHave; i++, iter++)
            {
                int value = (int)Mathf.Round(Random.Range(1, 2) * Random.value);
                switch (value)
                {
                    case 0:
                        _attackCoinsPlayer++;
                        coins[iter] = 1;
                            break;
                    case 1:
                        _defenceCoinsPlayer++;
                        coins[iter] = 2;
                            break;
                    default:
                        coins[iter] = 0;
                        break;
                }
            }

            for (int i = 0; i < coins.Length; i++)
            {
                switch (coins[i])
                {
                    case 1:
                        coinSlot[i].sprite = attack;
                        break;
                    case 2:
                        coinSlot[i].sprite = defense;
                        break;
                    default:
                        break;
                }
            }

            _attackText.text = "Attack: " + PLayerStats.AttackStats + "+" + _attackCoinsPlayer; // + " +" + _bonusAttackCoins;
            _defenceText.text = "Defence: " + PLayerStats.DefenseStats + "+" + _defenceCoinsPlayer; // + " +" + _bonusDefenceCoins;

            PLayerStats.AttackStats += _attackCoinsPlayer;
            PLayerStats.DefenseStats += _defenceCoinsPlayer;
            
            
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
            _buttonPressed = true;

        }

        void RefreshStats()
        {
            _buttonPressed = false;
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
            _attackCoinsPlayer = 0;
            _defenceCoinsPlayer = 0;

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
