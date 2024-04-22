using UnityEngine;

namespace Jake.Guards.States.Patrol
{
    public class PatrolIdle : WatchingState
    {
        private const float WAIT_TIME_MIN = 5.0f;
        private const float WAIT_TIME_MAX = 7.0f;
        private float _waitTime;
        
        public PatrolIdle(GuardScript guard) : base(guard)
        {
            _waitTime = Random.Range(WAIT_TIME_MIN, WAIT_TIME_MAX);
        }

        public override void OnEntered()
            => _guard.Animation.CrossFade(_guard.IdleAnim);

        public override void OnExited()
        {
        }

        public override void Update()
        {
            base.Update();
            
            if (_elapsed < _waitTime) return;
            _guard.SetState(new PatrolWalk(_guard));
        }
    }
}