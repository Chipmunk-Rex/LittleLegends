using LittleLegends.ConponentContainer;
using Unity.Netcode;
using UnityEngine;

namespace LittleLegends.Characters.States
{
    public abstract class CharacterAnimationState : CharacterStateSO
    {
        [SerializeField] private string _parameterName;
        private int _parameterHash;
        protected CharacterAnimationTrigger AnimationTrigger { get; private set; }
        protected CharacterAnimator CharacterAnimator { get; private set; }

        public override void Enter()
        {
            base.Enter();
            AnimationTrigger = StateMachine.Get<CharacterAnimationTrigger>();
            CharacterAnimator = StateMachine.Get<CharacterAnimator>();
            _parameterHash = Animator.StringToHash(_parameterName);
            CharacterAnimator.Animator.SetBool(_parameterHash, true);
        }

        public override void OnAnimationEnd()
        {
            base.OnAnimationEnd();
            CharacterAnimator.Animator.SetBool(_parameterHash, false);
        }

        public override void Exit()
        {
            base.Exit();
            CharacterAnimator.Animator.SetBool(_parameterHash, false);
        }
    }
}