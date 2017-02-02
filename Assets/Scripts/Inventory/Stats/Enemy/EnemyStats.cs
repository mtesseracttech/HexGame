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
        private bool _alive = true;
      //  public Image healthBar;

        void Update ()
        {
            GameOver();
        }

        public bool IsDead
        {
            set { _alive = value; }
            get { return _alive; }
        }

        public void DoDamageCombat(int attack, int defense)
        {
            if (attack > defense)
            {
              //  Debug.Log("damage done to enemy");
                DamageIsDone = Mathf.Abs(defense - attack);
                CurrentHealth -= DamageIsDone;
              //  Debug.Log("defense " + defense + "-"+"attack"+attack);
               // healthBar.fillAmount -= healthBar.fillAmount/ CurrentHealth;
                //Debug.Log(healthBar.fillAmount);
            }
        }

        void GameOver()
        {
            if (CurrentHealth <= 0)
            {
                _alive = false;
                //Debug.Log("enemy died");
                //destroy enemy
                //add quest number i guess
            }
        }
    }
}
