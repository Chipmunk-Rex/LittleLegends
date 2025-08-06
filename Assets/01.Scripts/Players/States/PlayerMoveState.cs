using LittleLegends.Characters;
using LittleLegends.Characters.States;
using UnityEngine;

namespace LittleLegends.Players.States
{
    [CreateAssetMenu(menuName = "States/PlayerMoveState")]
    public class PlayerMoveState : CharacterStateSO
    {
        [SerializeField] private PlayerInputSO _playerInputs;
        private CharacterMovement _characterMovement;
        public override void Initailize(CharacterStateMachine characterStateMachine)
        {
            base.Initailize(characterStateMachine);
            _characterMovement = GetComponent<CharacterMovement>();
        }

        public override void Enter()
        {
            base.Enter();
        }
        public override void Update()
        {
            base.Update();
            if (_playerInputs.MoveDirection != Vector2.zero)
            {
                Vector3 moveDirection = new Vector3(_playerInputs.MoveDirection.x, 0, _playerInputs.MoveDirection.y);
                _characterMovement.Move(moveDirection);
            }
            else
            {
                _characterMovement.Stop();
                StateMachine.ChangeState("IDLE");
            }
        }
    }
}