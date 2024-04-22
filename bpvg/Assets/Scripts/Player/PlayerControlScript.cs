using Jake.System;
using UnityEngine;
using UnityEngine.AI;

namespace Jake.Player
{
    public class PlayerControlScript : HaltableBehavior
    {
        [Header("~ Components")] 
        [SerializeField] private Transform _character;
        [SerializeField] private NavMeshAgent _movement;
        [SerializeField] private Animation _animation;
        [SerializeField] private Transform _camera;

        [Header("~ Animation")] 
        [SerializeField] private string _idleAnimation;
        [SerializeField] private string _walkAnimation;
        [SerializeField] private string _runAnimation;

        // Movement constants
        private const float WALK_SPEED = 1.0f;
        private const float RUN_SPEED = 6.0f;
        
        // Runtime variables
        private Vector3 _velocity;
        public bool IsSprinting { get; private set; }

        public override void UnhaltedUpdate()
        {
            // Calculate input delta
            var delta = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            var speed = Input.GetKey(KeyCode.LeftShift) ? RUN_SPEED : WALK_SPEED;

            // Is the player moving?
            if (delta.magnitude > Mathf.Epsilon)
            {
                // Calculate angle
                var angle = Mathf.Atan2(delta.x, delta.y) * Mathf.Rad2Deg + _camera.eulerAngles.y;
                _character.eulerAngles = new Vector3(0.0f, angle, 0.0f);
            
                // Move forward
                _movement.Move(_character.forward * (speed * Time.deltaTime));
                
                // Update flags
                IsSprinting = speed > WALK_SPEED;
                
                // Set animation
                _animation.CrossFade(IsSprinting ? _runAnimation : _walkAnimation);
                return;
            }
            
            // Set animation
            _animation.CrossFade(_idleAnimation);
            
            // Update flags
            IsSprinting = false;
        }

        public override void Halted()
        {
            // Switch to Idle animation
            _animation.CrossFade(_idleAnimation);
            
            // Update flags
            IsSprinting = false;
            
            // Disable movement
            _movement.isStopped = true;
        }

        public override void Resumed()
        {
            // Enable movement
            _movement.isStopped = false;
        }
    }
}