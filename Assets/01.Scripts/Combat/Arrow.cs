using TankCode.Projectiles;
using UnityEngine;

namespace _01.Scripts.Combat
{
    public class Arrow : ProjectileBase
    {
        protected override void OnCollisionEnter(Collision other)
        {
            base.OnCollisionEnter(other);
        }
    }
}