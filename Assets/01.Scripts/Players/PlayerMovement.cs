using System;
using LittleLegends.Characters;
using LittleLegends.ConponentContainer;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace LittleLegends.Players
{
    public class PlayerMovement : CharacterMovement
    {
        [SerializeField] private PlayerInputSO playerInputSo;
        public ComponentContainer ComponentContainer { get; set; }
        private bool _canMove = false;

        public NetworkVariable<Vector2> lookDirection =
            new NetworkVariable<Vector2>(Vector2.zero, NetworkVariableReadPermission.Everyone,
                NetworkVariableWritePermission.Owner);

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            // lookDirection.OnValueChanged += OnLookDirectionChanged;
        }

        private void OnLookDirectionChanged(Vector2 previousvalue, Vector2 newvalue)
        {
            characterController.transform.forward = new Vector3(newvalue.x, 0, newvalue.y);
        }

        public void EnableMovement() => _canMove = true;

        public void DisableMovement()
        {
            _canMove = false;
            Stop();
        }

        private void Update()
        {
            // LookMouse();
            if (IsOwner == false)
                return;
            RotateToCamera();
            if (_canMove && playerInputSo.MoveDirection != Vector2.zero)
            {
                Vector3 moveDirection = new Vector3(playerInputSo.MoveDirection.x, 0, playerInputSo.MoveDirection.y);
                Move(moveDirection);
            }
            else
            {
                Stop();
            }
        }

        private void RotateToCamera()
        {
            Vector3 eulerAngles = Camera.main.transform.eulerAngles;
            characterController.transform.rotation = Quaternion.Euler(0, eulerAngles.y, 0);
        }

        public void LookMouse()
        {
            Debug.Log("Look Mouse");
            Vector2 mouseDelta = playerInputSo.LookValue;
            if (mouseDelta == Vector2.zero)
                return;
            characterController.transform.eulerAngles += new Vector3(mouseDelta.x, 0, mouseDelta.y);

            // Vector2 mouseScreenPosition = playerInputSo.MousePosition;
            // Vector2 characterScreenPosition = Camera.main.WorldToScreenPoint(characterController.transform.position);
            // Vector2 direction = mouseScreenPosition - characterScreenPosition;
            // lookDirection.Value = direction.normalized;
        }
    }
}