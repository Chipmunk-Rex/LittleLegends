using System;
using LittleLegends.Characters;
using LittleLegends.ConponentContainer;
using UnityEngine;
using UnityEngine.Serialization;

namespace LittleLegends.Players
{
    public class PlayerMovement : CharacterMovement
    {
        [SerializeField] private PlayerInputSO playerInputSo;
        public ComponentContainer ComponentContainer { get; set; }
        private bool _canMove = false;

        private void Update()
        {
            if (_canMove && playerInputSo.MoveDirection != Vector2.zero)
            {
                Vector3 moveDirection = new Vector3(playerInputSo.MoveDirection.x, 0, playerInputSo.MoveDirection.y);
                Move(moveDirection);
            }
            else
            {
                Stop();
            }
            LookMouse();
        }

        public void EnableMovement() => _canMove = true;
        public void DisableMovement() { _canMove = false; Stop(); }

        public void LookMouse()
        {
            Vector2 mouseScreenPosition = playerInputSo.MousePosition;
            Vector2 characterScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 direction = mouseScreenPosition - characterScreenPosition;
            transform.forward = new Vector3(direction.x, 0, direction.y);
        }
    }
}