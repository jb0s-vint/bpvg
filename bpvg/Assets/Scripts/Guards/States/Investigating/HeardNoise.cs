using Jake.Guards.States.Patrol;
using UnityEngine;

namespace Jake.Guards.States.Investigating
{
    public class HeardNoise : WatchingState
    {
        private const float WAIT_TIME_MIN = 2.0f;
        private const float WAIT_TIME_MAX = 3.0f;
        private float _waitTime;
        private Vector3 _location;
        
        public HeardNoise(GuardScript guard, Vector3 location) : base(guard)
        {
            _waitTime = Random.Range(WAIT_TIME_MIN, WAIT_TIME_MAX);
            _location = location;
        }

        public override void Update()
        {
            base.Update();

            if (_elapsed > _waitTime)
            {
                // If we have waited a bit then start investigating the sound
                _guard.SetState(new InvestigatingNoise(_guard, _location));
            }
        }

        public override void OnEntered()
        {
            // Stop walking whereever we're going
            _guard.Animation.CrossFade(_guard.IdleAnim);
            _guard.NavMeshAgent.isStopped = true;
            
            // Look in the direction we heard the noise from
            _guard.transform.LookAt(_location);
            
            // Play question effect
            _guard.Character.Question();
        }

        public override void OnExited()
        {
        }
    }
}