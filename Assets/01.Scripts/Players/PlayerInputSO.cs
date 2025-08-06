using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace LittleLegends.Players
{
    [CreateAssetMenu(menuName = "Inputs/PlayerInput", order = 0)]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        public Controls Controls { get; private set; }
        public UnityEvent<Vector2> OnLookEvent = new UnityEvent<Vector2>();
        public UnityEvent OnAttackEvent = new UnityEvent();
        public UnityEvent OnInteractEvent = new UnityEvent();
        public UnityEvent OnCrouchEvent = new UnityEvent();
        public UnityEvent OnJumpEvent = new UnityEvent();
        public UnityEvent OnPreviousEvent = new UnityEvent();
        public UnityEvent OnNextEvent = new UnityEvent();
        public UnityEvent OnSprintEvent = new UnityEvent();

        public Vector2 MousePosition { get; private set; }

        public Vector2 MouseWorldPosition =>
            Camera.main.ScreenToWorldPoint(new Vector3(MousePosition.x, MousePosition.y, Camera.main.nearClipPlane));


        public Vector2 MoveDirection { get; private set; }
        public Vector2 LookValue { get; private set; }

        private void OnEnable()
        {
            if (Controls == null)
            {
                Controls = new Controls();
                Controls.Player.SetCallbacks(this);
            }

            Controls.Enable();
        }
        private void OnDisable()
        {
            Controls.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveDirection = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            LookValue = context.ReadValue<Vector2>();
            OnLookEvent.Invoke(LookValue);
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnAttackEvent.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnInteractEvent.Invoke();
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnCrouchEvent.Invoke();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnJumpEvent.Invoke();
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnPreviousEvent.Invoke();
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnNextEvent.Invoke();
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSprintEvent.Invoke();
        }

        public void OnMouse(InputAction.CallbackContext context)
        {
            MousePosition = context.ReadValue<Vector2>();
        }
    }
}