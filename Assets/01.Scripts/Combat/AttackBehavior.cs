using LittleLegends.ConponentContainer;
using LittleLegends.StatSystem;
using Unity.Netcode;
using UnityEngine;

namespace LittleLegends.Combat
{
    public class AttackBehavior : NetworkBehaviour, IContainerComponent, IAfterInitailze
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

        public void Attack(Vector3 direction)
        {
            AttackServerRPC(direction);
        }

        [ServerRpc(RequireOwnership = true)]
        private void AttackServerRPC(Vector3 direction)
        {
            OnServerAttack(direction);
            AttackClientRPC(direction);
        }

        protected virtual void OnServerAttack(Vector3 direction)
        {
        }

        [ClientRpc]
        private void AttackClientRPC(Vector3 direction)
        {
            if (IsServer)
                return;
            OnClientAttack(direction);
        }

        protected virtual void OnClientAttack(Vector3 direction)
        {
        }
    }
}