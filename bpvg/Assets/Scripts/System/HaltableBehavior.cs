using System;
using UnityEngine;

namespace Jake.System
{
    public abstract class HaltableBehavior : MonoBehaviour
    {
        /// <summary>
        /// Is this HaltableBehavior in its Halted state?
        /// </summary>
        protected bool _isHalted { get; private set; }

        /// <summary>
        /// Halt or resume this HaltableBehavior.
        /// </summary>
        /// <param name="halt">Should the HaltableBehavior be halted or resumed?</param>
        public void SetHalted(bool halt)
        {
            _isHalted = true;
            if(_isHalted) Halted();
            else Resumed();
        }

        private void Start()
        {
            _isHalted = false;
            Resumed();
        }

        private void Update()
        {
            if (_isHalted) return;
            UnhaltedUpdate();
        }
        
        /// <summary>
        /// Called when the HaltableBehavior is being halted.
        /// </summary>
        public abstract void Halted();
        
        /// <summary>
        /// Called when the HaltableBehavior is being unhalted.
        /// </summary>
        public abstract void Resumed();

        /// <summary>
        /// Called every Update if the behavior is not halted.
        /// </summary>
        public virtual void UnhaltedUpdate()
        {
            return;
        }
    }
}