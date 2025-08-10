using LittleLegends.ConponentContainer;
using LittleLegends.StatSystem;
using UnityEngine;

namespace LittleLegends.Combat
{
    public class AttackBehavior : MonoBehaviour, IContainerComponent, IAfterInitailze
    {
        [Header("References")] [SerializeField]
        private StatSO damageStat;

        [SerializeField] Transform attackPoint;
        public ComponentContainer ComponentContainer { get; set; }

        public void OnInitialize(ComponentContainer componentContainer)
        {
            if (attackPoint == null)
                attackPoint = transform;
        }

        public void AfterInit()
        {
            damageStat = this.Get<StatBehavior>().GetStat(damageStat);
        }

        public virtual void Attack(Vector3 direction)
        {
        }
    }
}