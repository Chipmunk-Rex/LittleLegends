using UnityEngine;

namespace LittleLegends.Combat
{
    public class ProjectileAttackBehavior : AttackBehavior
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float projectileSpeed = 10f;

        public override void Attack(Vector3 direction)
        {
            if (projectilePrefab == null)
            {
                Debug.LogError("Projectile prefab is not assigned.");
                return;
            }

            // Instantiate the projectile at the attacker's position and rotation
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Get the Rigidbody component of the projectile
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Set the velocity of the projectile in the specified direction
                rb.linearVelocity = direction.normalized * projectileSpeed;
            }
            else
            {
                Debug.LogError("Projectile prefab does not have a Rigidbody component.");
            }
        }
    }
}