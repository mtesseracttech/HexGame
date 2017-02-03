using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory.Stats.Enemy
{
    public class EnemyStats : MonoBehaviour {

        public int CurrentHealth;
        public int AttackStats;
        public int DefenseStats;
        public int CoinHave;
        public int DamageIsDone;
        private bool _dead;
      //  public Image healthBar;

        void Update ()
        {
            GameOver();
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
            }
        }

        public bool IsDead
        {
            set { _dead = value; }
            get { return _dead; }
        }

        public void DoDamageCombat(int attack, int defense)
        {
            if (attack > defense)
            {
                //Debug.Log("damage done to enemy");
                DamageIsDone = Mathf.Abs(defense - attack);
                CurrentHealth -= DamageIsDone;
                //Debug.Log("defense " + defense + "-"+"attack"+attack);
                //healthBar.fillAmount -= healthBar.fillAmount/ CurrentHealth;
                //Debug.Log(healthBar.fillAmount);
            }
        }

        void GameOver()
        {
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                _dead = true;
                //Debug.Log("enemy died");
                //destroy enemy
                //add quest number i guess
            }
        }
    }
}
