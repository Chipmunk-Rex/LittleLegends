using LittleLegends.ConponentContainer;
using LittleLegends.StatSystem;
using UnityEngine;

namespace LittleLegends.Combat
{
    public class Health : MonoBehaviour, IContainerComponent, IAfterInitailze
    {
        [SerializeField] private StatSO healthStat;
        private float currentHealth;
        public float CurrentHealth => currentHealth;


        public ComponentContainer ComponentContainer { get; set; }

        public void OnInitialize(ComponentContainer componentContainer)
        {
        }

        public void AfterInit()
        {
            healthStat = this.Get<StatBehavior>().GetStat(healthStat);
            currentHealth = healthStat.Value;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log($"{gameObject.name} has died.");
        }
    }
}