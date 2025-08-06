using LittleLegends.ConponentContainer;
using LittleLegends.StatSystem;
using UnityEngine;

namespace LittleLegends.Combat
{
    public class AttackBehavior : MonoBehaviour, IContainerComponent, IAfterInitailze
    {
        [Header("References")] [SerializeField]
        private StatSO damageStat;

        public ComponentContainer ComponentContainer { get; set; }

        public void OnInitialize(ComponentContainer componentContainer)
        {
        }

        public void AfterInit()
        {
            damageStat = this.Get<StatBehavior>().GetStat(damageStat);
        }

        public void Attack(Vector3 position)
        {
            
        }
    }
}