using TankCode.Projectiles;
using UnityEngine;

namespace LittleLegends.Combat
{
    public class ProjectileAttackBehavior : AttackBehavior
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float projectileSpeed = 10f;
        [SerializeField] ProjectileBase clientProjectilePrefab;
        [SerializeField] ProjectileBase serverProjectilePrefab;

        protected override void OnServerAttack(Vector3 direction)
        {
            base.OnServerAttack(direction);
            ProjectileBase serverProjectile = Instantiate(serverProjectilePrefab);
            serverProjectile.transform.position = transform.position;
            serverProjectile.FireProjectile(direction);
        }

        protected override void OnClientAttack(Vector3 direction)
        {
            base.OnClientAttack(direction);
            ProjectileBase projectile = Instantiate(clientProjectilePrefab);
            projectile.transform.position = transform.position;
            projectile.FireProjectile(direction);
        }
    }
}