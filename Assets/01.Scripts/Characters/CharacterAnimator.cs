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

        [Header("References")]
        [field: SerializeField]
        public Animator Animator { get; private set; }

        [SerializeField] private StatSO _moveSpeedStat;

        [Header("Settings")] [SerializeField] private float _lerpSpeed = 1f;
        Vector2 _animationDirection;
        Vector2 _currentDirection;
        public ComponentContainer ComponentContainer { get; set; }

        public void OnInitialize(ComponentContainer componentContainer)
        {
        }

        public void AfterInit()
        {
            // 파라미터 이름 못정해서 그냥 string으로 바꿈
            _moveSpeedStat = this.Get<StatBehavior>().GetStat(_moveSpeedStat);
            Animator.SetFloat("MoveSpeed", _moveSpeedStat.Value);
            _moveSpeedStat.OnValueChange += OnMoveSpeedChange;
        }

        private void OnMoveSpeedChange(StatSO stat, float current, float previous)
        {
            Animator.SetFloat("MoveSpeed", current);
        }

        public void SetDirection(Vector3 direction)
        {
            // 월드좌표계 -> 로컬 좌표계
            // Vector3 localDirection = Quaternion.LookRotation(transform.forward) * direction;
            Vector3 localDirection = direction;
            _currentDirection = new Vector2(localDirection.x, localDirection.z).normalized;
            Animator.SetFloat("MoveSpeedNormal", direction == Vector3.zero ? 0 : 1);
        }

        private void Update()
        {
            _animationDirection = Vector2.Lerp(_animationDirection, _currentDirection, Time.deltaTime / _lerpSpeed);
            Animator.SetFloat(_moveXHash, _animationDirection.x);
            Animator.SetFloat(_moveYHash, _animationDirection.y);
        }
    }
}