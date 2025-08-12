using System;
using UnityEngine;

namespace TankCode.Projectiles
{
    public class ProjectileBase : MonoBehaviour
    {
        [SerializeField] private float lifeTime = 2f;
        [field: SerializeField] public Rigidbody RBCompo { get; private set; }
        [field: SerializeField] public Collider ColliderCompo { get; private set; }

        private float _currentLifeTime;

        protected virtual void Update()
        {
            _currentLifeTime += Time.deltaTime;

            if (_currentLifeTime > lifeTime)
                DestroyGameObject();
        }

        public virtual void FireProjectile(Vector2 velocity)
        {
            RBCompo.linearVelocity = velocity;
        }

        protected virtual void OnCollisionEnter(Collision other)
        {
            DestroyGameObject();
        }

        protected virtual void DestroyGameObject()
        {
            Destroy(gameObject);
        }
    }
}