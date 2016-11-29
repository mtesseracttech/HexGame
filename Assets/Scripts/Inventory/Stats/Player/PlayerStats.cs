using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory.Stats.Player
{
    public class PlayerStats : MonoBehaviour
    {
        //MaxHealth
        //currentHealth
        //RadiationEffecct (decreasin max health)
        //Attack we make
        //Defense we use
        //coins(attak and defense)
        //Damage
        public int MaximumHealth;
        public int CurrentHealth;
        public int AttackStats;
        public int DefenseStats;
        public int CoinHave;
        public int DamageIsDone;
        public int Radiation;

        public Image healthBar;

        void Update()
        {
            GameOver();
        }

        public int RadiationEffect
        {
            get { return Radiation;}
        }

        public void ReduceMaxHealth()
        {
            if (Radiation > 0)
            {
                MaximumHealth -= Radiation;
            }
        }

        public void DoDamageCombat(int attack, int defense)
        {
            if (attack > defense)
            {
                Debug.Log("damage done to player");
                DamageIsDone = Mathf.Abs(defense - attack);
                CurrentHealth -= DamageIsDone;
                healthBar.fillAmount -= healthBar.fillAmount / CurrentHealth;
            }
        
        }

        void GameOver()
        {
            if (CurrentHealth <= 0)
            {
                Debug.Log("gameOVer");
            }
        }
    }
}
