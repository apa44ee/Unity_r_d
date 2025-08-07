using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Dreamteck.Splines;
using Dreamteck.Forever;

namespace Controllers.Input
{
    public class InputController : IDisposable
    {
        private readonly Controller _inputActions;
        public Controller InputActions => _inputActions;

        public event Action<Vector2> MovementReceived;
        public event Action MovementCanceled;
        public event Action MenuButtonPressed;
        public event Action JumpStarted;

        public InputController()
        {
            _inputActions = new Controller();
            _inputActions.Enable();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _inputActions.Default.Movement.performed += OnMovementPerformed;
            _inputActions.Default.Movement.canceled += OnMovementCanceled;
            _inputActions.Default.MenuButton.performed += OnMenuPressed;
            _inputActions.Default.Jump.performed += OnJumpPressed;
        }

        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            MovementReceived?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnMovementCanceled(InputAction.CallbackContext context)
        {
            MovementCanceled?.Invoke();
        }
        
        private void OnMenuPressed(InputAction.CallbackContext context)
        {
            MenuButtonPressed?.Invoke();
        }
        
        private void OnJumpPressed(InputAction.CallbackContext context)
        {
            JumpStarted?.Invoke();
        }

        public void Dispose()
        {
            _inputActions.Default.Movement.performed -= OnMovementPerformed;
            _inputActions.Default.Movement.canceled -= OnMovementCanceled;
            _inputActions.Default.MenuButton.performed -= OnMenuPressed;
            _inputActions.Default.Jump.performed -= OnJumpPressed;
        }
    }
}