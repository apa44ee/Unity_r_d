using System.Collections;
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

        [SerializeField] private Animator _animator;
        private const string RunningBool = "isRunning";
        private const string JumpingTrigger = "Jump";
        
        private const string CollisionTrigger = "Hit";
        private bool _isStunned = false;
        [SerializeField] private float _collisionStunDuration = 1f;
        
        [SerializeField] private float _jumpHeight = 2f;
        [SerializeField] private float _jumpDuration = 0.5f;
        private bool _isJumping = false;
        private float _jumpTimer = 0f;

        private void Awake()
        {
            _animator.SetBool(RunningBool, true);
        }

        private void OnEnable()
        {
            _inputController = new InputController();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _inputController.MovementReceived += OnMovementReceived;
            _inputController.MovementCanceled += OnMovementCanceled;
            _inputController.JumpStarted += OnJumpStarted;
        }

        private void UnsubscribeEvents()
        {
            _inputController.MovementReceived -= OnMovementReceived;
            _inputController.MovementCanceled -= OnMovementCanceled;
            _inputController.JumpStarted -= OnJumpStarted;
        }

        private void OnMovementReceived(Vector2 movement)
        {
            _targetX = movement.x;
        }

        private void OnMovementCanceled()
        {
            _targetX = 0f;
        }

        private void OnJumpStarted()
        {
            if (!_isJumping)
            {
                _isJumping = true;
                _jumpTimer = 0f;
                _animator.SetTrigger(JumpingTrigger);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Obstacle") && !_isStunned)
            {
                StartCoroutine(HandleCollision());
            }
        }
        
        private IEnumerator HandleCollision()
        {
            _isStunned = true;
            _basicRunner.enabled = false;
            
            _animator.SetTrigger(CollisionTrigger);
            
            yield return new WaitForSeconds(_collisionStunDuration);
            
            
            _isStunned = false;
            _basicRunner.enabled = true;
        }

        private void Update()
        {
            if (_basicRunner == null) return;
            
            if (!_isStunned)
            {
                float currentOffsetX = _basicRunner.motion.offset.x;
                float newOffsetX = currentOffsetX + _targetX * _lateralSpeed * Time.deltaTime;
                newOffsetX = Mathf.Clamp(newOffsetX, -_lateralRange, _lateralRange);
                
                float yOffset = 0f;
                if (_isJumping)
                {
                    _jumpTimer += Time.deltaTime;
                    float progress = _jumpTimer / _jumpDuration;
                    
                    if (progress >= 1f)
                    {
                        _isJumping = false;
                    }
                    else
                    {
                        yOffset = _jumpHeight * 4 * (progress - progress * progress);
                    }
                }
                _basicRunner.motion.offset = new Vector2(newOffsetX, yOffset);
            }
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
            _inputController.Dispose();
        }
    }
}