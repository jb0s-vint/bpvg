using UnityEngine;

namespace Jake.Guards
{
    public abstract class GuardState
    {
        protected GuardScript _guard;
        protected float _elapsed;
        
        public GuardState(GuardScript guard)
        {
            _guard = guard;
        }

        public void Tick()
        {
            _elapsed += Time.deltaTime;
            Update();
        }
        
        /// <summary>
        /// Called when the GuardScript enters this state.
        /// </summary>
        public abstract void OnEntered();
        
        /// <summary>
        /// Called when the GuardScript exits this state.
        /// </summary>
        public abstract void OnExited();
        
        /// <summary>
        /// Called every frame while the GuardScript is in this state.
        /// </summary>
        public abstract void Update();
    }
}