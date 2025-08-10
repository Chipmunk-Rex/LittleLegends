using LittleLegends.Characters.States;
using LittleLegends.Combat;
using LittleLegends.ConponentContainer;
using UnityEngine;

namespace LittleLegends.Players.States
{
    [CreateAssetMenu(menuName = "States/PlayerAttackState")]
    public class PlayerAttackState : CharacterAnimationState
    {
        private AttackBehavior AttackBehavior;
        private PlayerMovement _characterMovement;
        [SerializeField] private PlayerInputSO _playerInputs;

        public override void Enter()
        {
            base.Enter();
            AttackBehavior = StateMachine.Get<AttackBehavior>(true);
            _characterMovement = StateMachine.Get<PlayerMovement>();
            _characterMovement.EnableMovement();
        }
        public override void Exit()
        {
            base.Exit();
            if (_characterMovement != null)
                _characterMovement.DisableMovement();
        }

        public override void OnAnimationTrigger()
        {
            base.OnAnimationTrigger();
            AttackBehavior.Attack(AttackBehavior.transform.forward);
        }

        public override void OnAnimationEnd()
        {
            base.OnAnimationEnd();
            StateMachine.ChangeState("IDLE");
        }
    }
}