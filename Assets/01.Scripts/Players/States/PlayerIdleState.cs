using LittleLegends.Characters;
using LittleLegends.Characters.States;
using LittleLegends.ConponentContainer;
using UnityEngine;

namespace LittleLegends.Players.States
{
    [CreateAssetMenu(menuName = "States/PlayerIdleState")]
    public class PlayerIdleState : CharacterIdleState
    {
        [SerializeField] private PlayerInputSO _playerInputs;
        [SerializeField] private PlayerMovement _playerMovement;

        public override void Initailize(CharacterStateMachine characterStateMachine)
        {
            base.Initailize(characterStateMachine);
            _playerMovement = characterStateMachine.Get<PlayerMovement>();
        }

        public override void Enter()
        {
            base.Enter();
            _playerMovement.EnableMovement();
            _playerInputs.OnAttackEvent.AddListener(OnAttackHandler);
        }

        private void OnAttackHandler()
        {
            StateMachine.ChangeState("ATTACK");
        }

        public override void Update()
        {
            base.Update();
            if (_playerInputs.MoveDirection != Vector2.zero)
            {
                StateMachine.ChangeState("MOVE");
            }
        }

        public override void Exit()
        {
            base.Exit();
            _playerMovement.DisableMovement();
            _playerInputs.OnAttackEvent.RemoveListener(OnAttackHandler);
        }
    }
}