using Controllers.Input;
using UnityEngine;
using Dreamteck.Splines;
using Dreamteck.Forever;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        private InputController _inputController;

        [SerializeField] private Runner _basicRunner;

        private float _targetX;
        
        [SerializeField] private float _lateralSpeed = 5f;
        [SerializeField] private float _lateralRange = 5f;

        private void OnEnable()
        {
            _inputController = new InputController();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _inputController.MovementReceived += OnMovementReceived;
            _inputController.MovementCanceled += OnMovementCanceled;
        }

        private void UnsubscribeEvents()
        {
            _inputController.MovementReceived -= OnMovementReceived;
            _inputController.MovementCanceled -= OnMovementCanceled;
        }

        private void OnMovementReceived(Vector2 movement)
        {
            _targetX = movement.x;
        }

        private void OnMovementCanceled()
        {
            _targetX = 0f;
        }

        private void Update()
        {
            if (_basicRunner != null)
            {
                float currentOffsetX = _basicRunner.motion.offset.x;
                
                float newOffsetX = currentOffsetX + _targetX * _lateralSpeed * Time.deltaTime;
                
                newOffsetX = Mathf.Clamp(newOffsetX, -_lateralRange, _lateralRange);
                
                _basicRunner.motion.offset = new Vector2(newOffsetX, _basicRunner.motion.offset.y);
            }
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
            _inputController.Dispose();
        }
    }
}