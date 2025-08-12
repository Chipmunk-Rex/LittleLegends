using LittleLegends.Combat;
using UnityEngine;

namespace TankCode.Projectiles
{
    public class ServerProjectile : ProjectileBase
    {
        // [SerializeField] ClientProjectile
        [SerializeField] public int damage = 1;

        public override void FireProjectile(Vector2 velocity)
        {
            base.FireProjectile(velocity);
        }
    }
}