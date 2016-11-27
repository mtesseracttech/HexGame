using UnityEngine;

namespace Assets.Scripts.Inventory.Stats.Enemy
{
    public class EnemyStats : MonoBehaviour {

        public int CurrentHealth;
        public int AttackStats;
        public int DefenseStats;
        public int CoinHave;
        public int DamageIsDone;

        void Update ()
        {
            GameOver();
        }

        public void DoDamageCombat(int attack, int defense)
        {
            if (attack > defense)
            {
                Debug.Log("damage done to enemy");
                DamageIsDone = Mathf.Abs(defense - attack);
                CurrentHealth -= DamageIsDone;
                Debug.Log("defense " + defense + "-"+"attack"+attack);
            }
        }

        void GameOver()
        {
            if (CurrentHealth <= 0)
            {
                //Debug.Log("enemy died");
                //destroy enemy
                //add quest number i guess
            }
        }
    }
}
