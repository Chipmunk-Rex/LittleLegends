using LittleLegends.Combat;
using UnityEngine;

namespace TankCode.Projectiles
{
    public class ServerProjectile : ProjectileBase
    {
        [SerializeField] public int damage = 1;

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.attachedRigidbody.TryGetComponent(out Health health))
            {
                health.TakeDamage(damage);
            }

            base.OnTriggerEnter2D(other);
        }
    }
}