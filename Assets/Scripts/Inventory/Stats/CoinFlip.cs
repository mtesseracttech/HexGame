using System.Collections;
using Assets.Scripts.Inventory.Stats.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory.Stats
{
    public class CoinFlip : MonoBehaviour
    {
        public GameObject attackButton;
        public Sprite attack;
        public Sprite defense;
        public Sprite defaultIcon;
        [Space]
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

        public Player.PlayerStats PlayerStats;
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

        public Image _enemyImage;

        private int AttackCoinsEnemy;
        private int DefenceCoinsEnemy;

        private int _defAttackAmountEnemy;
        private int _defDefenseAmountEnemy;
        private int _defCoinAmountEnemy;

       
        public EnemyStats EnemyStats;//get enemie that we attack
        private bool _attackEnds;
        

        private void Start()
        {
            //loop through how many slots we have.
            //then check how many coins we have and then fill the slots with default image
            //when flipped update the slot image with attack or defense
            for (int i = 0; i < coinSlot.Length; i++)
            {
                coinSlot[i].sprite = defaultIcon;
            }
        }

        public bool AttackButton
        {
            set { _buttonPressed = value; }
            get { return _buttonPressed; }
        }

        public bool AttackEnds
        {
            set { _attackEnds = value; }
            get { return _attackEnds;}
        }

        

        private void Update()
        {
            //defining player stats for reseting
            _defAttackAmountPlayer  = PlayerStats.AttackStats;
            _defDefenseAmountPlayer = PlayerStats.DefenseStats;
            _defCoinAmountPlayer    = PlayerStats.CoinHave;

            //defining player stats for reseting
            _defAttackAmountEnemy   = EnemyStats.AttackStats;
            _defDefenseAmountEnemy  = EnemyStats.DefenseStats;
            _defCoinAmountEnemy     = EnemyStats.CoinHave;

            /**/
            RefreshStats();

            _attackText.text = "Attack   : " + PlayerStats.AttackStats;
            _defenceText.text = "Defence: " + PlayerStats.DefenseStats;

            _attackTextEnemy.text =  "Attack : " + EnemyStats.AttackStats;
            _defenceTextEnemy.text = "Defence: " + EnemyStats.DefenseStats;
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


            for (int i = 0; i < PlayerStats.CoinHave; i++, iter++)
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

            _attackText.text =  "Attack : " + PlayerStats.AttackStats + "+" + _attackCoinsPlayer; // + " +" + _bonusAttackCoins;
            _defenceText.text = "Defence: " + PlayerStats.DefenseStats + "+" + _defenceCoinsPlayer; // + " +" + _bonusDefenceCoins;

            PlayerStats.AttackStats += _attackCoinsPlayer;
            PlayerStats.DefenseStats += _defenceCoinsPlayer;
            
            
           // Debug.Log("Player Attack coin: " + AttackCoinsPlayer + "Player Defence coin: " + DefenceCoinsPlayer);


            //--------------------------------------------------------------------//
            //             ENEMY flip                                            //
            for (int i = 0; i < EnemyStats.CoinHave; i++)
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
            _attackTextEnemy.text = "Attack : " + EnemyStats.AttackStats + "+" + AttackCoinsEnemy; // + " +" + _bonusAttackCoins;
            _defenceTextEnemy.text = "Defence : "+ EnemyStats.DefenseStats + "+" +  DefenceCoinsEnemy; // + " +" + _bonusDefenceCoins;

            EnemyStats.AttackStats += AttackCoinsEnemy;
            EnemyStats.DefenseStats += DefenceCoinsEnemy;

            RefreshStats();
            //do damge


            StartCoroutine(DoDamage());


        }

        IEnumerator DoDamage()
        { 
             //do damage to player
            PlayerStats.DoDamageCombat(EnemyStats.AttackStats, PlayerStats.DefenseStats);

            //do damage to enemy
            EnemyStats.DoDamageCombat(PlayerStats.AttackStats, EnemyStats.DefenseStats);
            RefreshStats();

            yield return new WaitForSeconds(1f);
            ResetValues();

            _buttonPressed = true;
            _attackEnds = true;
            attackButton.SetActive(true);
            

        }

        void RefreshStats()
        {
            _buttonPressed = false;
            //-----------------------------------------------------------//
            //                          PLAYER                          //
            _healthText.text = "Health : " + PlayerStats.CurrentHealth;
            _coins.text =      "Coins  : " + PlayerStats.CoinHave;
            _damageDone.text = "DamageDone: " + PlayerStats.DamageIsDone;

            //-----------------------------------------------------------//
            //                         ENEMY                            //
            _healthTextEnemy.text = "Health: " + EnemyStats.CurrentHealth;
            _coinsEnemy.text = "Coins: " + EnemyStats.CoinHave;
            _damageDoneEnemy.text = "DamageDone: " + EnemyStats.DamageIsDone;
        }

        void ResetValues()
        {
            //-----------------------------------------------------------//
            //                          PLAYER                          //
            PlayerStats.CoinHave = _defCoinAmountPlayer;
            PlayerStats.AttackStats = _defAttackAmountPlayer;
            PlayerStats.DefenseStats = _defDefenseAmountPlayer;
            _attackCoinsPlayer = 0;
            _defenceCoinsPlayer = 0;

            //-----------------------------------------------------------//
            //                         ENEMY                            //
            EnemyStats.CoinHave = _defCoinAmountEnemy;
            EnemyStats.AttackStats = _defAttackAmountEnemy;
            EnemyStats.DefenseStats = _defDefenseAmountEnemy;
            AttackCoinsEnemy = 0;
            DefenceCoinsEnemy = 0;
        }

    }
}
