using LittleLegends.Characters;
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

        public override void Initailize(CharacterStateMachine characterStateMachine)
        {
            base.Initailize(characterStateMachine);
            AttackBehavior = StateMachine.Get<AttackBehavior>(true);
            _characterMovement = StateMachine.Get<PlayerMovement>();
        }

        public override void Enter()
        {
            base.Enter();
            if (IsOwner)
                _characterMovement.EnableMovement();
        }

        public override void Exit()
        {
            base.Exit();
            if (IsOwner)
                _characterMovement.DisableMovement();
        }

        public override void OnAnimationTrigger()
        {
            base.OnAnimationTrigger();
            if (IsOwner)
                AttackBehavior.Attack(AttackBehavior.transform.forward);
        }

        public override void OnAnimationEnd()
        {
            base.OnAnimationEnd();
            StateMachine.ChangeState("IDLE");
        }
    }
}