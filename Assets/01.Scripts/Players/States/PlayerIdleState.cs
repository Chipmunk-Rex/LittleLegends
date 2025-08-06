using LittleLegends.Characters.States;
using UnityEngine;

namespace LittleLegends.Players.States
{
    [CreateAssetMenu(menuName = "States/PlayerIdleState")]
    public class PlayerIdleState : CharacterIdleState
    {
        [SerializeField] private PlayerInputSO _playerInputs;
        public override void Enter()
        {
            base.Enter();
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
        }
    }
}