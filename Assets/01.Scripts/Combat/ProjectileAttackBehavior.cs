using UnityEngine;

namespace LittleLegends.Combat
{
    public class ProjectileAttackBehavior : AttackBehavior
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float projectileSpeed = 10f;

        public override void ExecuteAttack(Transform attacker, Vector3 targetPosition)
        {
            if (projectilePrefab == null)
            {
                Debug.LogError("Projectile prefab is not assigned.");
                return;
            }

            GameObject projectile = Instantiate(projectilePrefab, attacker.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (targetPosition - attacker.position).normalized;
                rb.velocity = direction * projectileSpeed;
            }
            else
            {
                Debug.LogError("Projectile prefab does not have a Rigidbody component.");
            }
        }
    }
}