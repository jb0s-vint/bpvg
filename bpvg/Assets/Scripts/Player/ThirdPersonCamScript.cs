using Jake.System;
using UnityEngine;

namespace Jake.Player
{
    public class ThirdPersonCamScript : HaltableBehavior
    {
        // Camera constraint constants
        private const float MIN_DISTANCE = 1.0f;
        private const float MAX_DISTANCE = 3.0f;
        private const float POSITION_INCREASE = 6.0f;
        
        // Camera sensitivity constants
        private const float MOUSE_SPEED = 300.0f;
        private const float ANGLE_RANGE = 85.0f;
        
        // Configuration
        [SerializeField] private Transform _pivot;
        [SerializeField, Range(MIN_DISTANCE, MAX_DISTANCE)] private float _distance = MIN_DISTANCE;
        
        // Runtime variables
        private float _xRotation, _yRotation;

        public override void UnhaltedUpdate()
        {
            // Get input delta from mouse
            var mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            
            // Apply input delta to camera rotation
            _xRotation += mouseDelta.y * MOUSE_SPEED * Time.deltaTime;
            _yRotation += mouseDelta.x * MOUSE_SPEED * Time.deltaTime;
            
            // Clamp vertical camera angle within range
            _xRotation = _xRotation switch
            {
                > ANGLE_RANGE => ANGLE_RANGE,    // Clamp the maximum vertical angle to +ANGLE_RANGE
                < -ANGLE_RANGE => -ANGLE_RANGE,  // Clamp the minimum vertical angle to -ANGLE_RANGE
                _ => _xRotation                  // Rotation is within bounds, don't clamp
            };
            
            // Make sure horizontal camera angle does not exceed 360 deg
            _yRotation %= 360.0f;
            
            // Apply rotation to camera pivot and relocate camera
            _pivot.rotation = Quaternion.Euler(_xRotation, _yRotation, 0.0f);
            transform.position = _pivot.position - (_pivot.forward * _distance);
            transform.LookAt(_pivot);
        }

        /// <summary>
        /// Unlock the mouse cursor when halted
        /// </summary>
        public override void Halted()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        /// <summary>
        /// Lock the mouse cursor when resumed
        /// </summary>
        public override void Resumed()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}