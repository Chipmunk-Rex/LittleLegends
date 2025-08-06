using System;
using LittleLegends.ConponentContainer;
using LittleLegends.StatSystem;
using UnityEngine;

namespace LittleLegends.Characters
{
    public class CharacterAnimator : MonoBehaviour, IContainerComponent, IAfterInitailze
    {
        int _moveXHash = Animator.StringToHash("MoveX");
        int _moveYHash = Animator.StringToHash("MoveY");

        [Header("References")] [SerializeField]
        private Animator _animator;

        [SerializeField] private StatSO _moveSpeedStat;

        [Header("Settings")] [SerializeField] private float _lerpSpeed = 1f;
        Vector2 _animationDirection;
        Vector2 _moveDirection;
        public ComponentContainer ComponentContainer { get; set; }

        public void OnInitialize(ComponentContainer componentContainer)
        {
        }

        public void AfterInit()
        {
            _moveSpeedStat = this.Get<StatBehavior>().GetStat(_moveSpeedStat);
            _animator.SetFloat("MoveSpeed", _moveSpeedStat.Value);
            _moveSpeedStat.OnValueChange += OnMoveSpeedChange;
        }

        private void OnMoveSpeedChange(StatSO stat, float current, float previous)
        {
            _animator.SetFloat("MoveSpeed", current);
        }

        public void SetDirection(Vector3 direction)
        {
            Vector3 localDirection = Quaternion.LookRotation(transform.forward) * direction;
            _moveDirection = new Vector2(localDirection.x, localDirection.z).normalized;
            _animator.SetFloat("MoveSpeedNormal", direction == Vector3.zero ? 0 : 1);

            // _animator.SetFloat(_moveXHash, direction.x);
            // _animator.SetFloat(_moveYHash, direction.z);
        }

        private void Update()
        {
            _animationDirection = Vector2.Lerp(_animationDirection, _moveDirection, Time.deltaTime / _lerpSpeed);
            _animator.SetFloat(_moveXHash, _animationDirection.x);
            _animator.SetFloat(_moveYHash, _animationDirection.y);
        }
    }
}