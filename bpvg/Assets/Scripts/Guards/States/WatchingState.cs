using UnityEngine;
using Jake.System;
using Jake.Guards.States.Investigating;
using Jake.Guards.States.Patrol;

namespace Jake.Guards.States
{
    public abstract class WatchingState : GuardState
    {
        // Consciousness constants
        private const float VIEW_RANGE = 8.0f;
        private const float HEAR_RANGE = 9.0f;
        
        // References
        private PlayerControlScript _player;
        
        protected WatchingState(GuardScript guard) : base(guard)
        {
            _player = Object.FindObjectOfType<PlayerControlScript>();
        }

        public override void Update()
        {
            // Are we not chasing the player?
            if (_guard.CurrentState is not ChasingState)
            {
                // Do we hear the player?
                if (CanHearPlayer() && _guard.CurrentState is not HeardNoise and not InvestigatingNoise)
                {
                    Debug.Log("Player was heard!");
                    _guard.Awareness += 5;
                    _guard.SetState(new HeardNoise(_guard, _player.transform.position));
                    
                    // Don't let inheriting state override our changes here
                    return;
                }
            
                // Do we see the player?
                if (CanSeePlayer() && _guard.CurrentState is not ChasingState)
                {
                    Debug.Log("Player was spotted!");
                    _guard.Awareness += Random.Range(3.0f, 8.5f);
                    _guard.SetState(new ChasingState(_guard));
                
                    // Don't let inheriting state override our changes here
                    return;
                }
            }
        }

        protected bool CanHearPlayer()
        {
            // Is the player within hearing distance?
            float dist = Vector3.Distance(_guard.transform.position, _player.transform.position);
            if (dist > HEAR_RANGE) return false;
            
            // Is the player currently running?
            return _player.IsSprinting;
        }
        
        protected bool CanSeePlayer()
        {
            // Is the player within viewing distance?
            float dist = Vector3.Distance(_guard.transform.position, _player.transform.position);
            if (dist > VIEW_RANGE) return false;
            
            // Is the player behind us?
            var delta = _player.transform.position - _guard.transform.position;
            var dotProduct = Vector3.Dot(delta, _guard.transform.forward);
            if (dotProduct < 0) return false;
            
            // Is the player behind a wall?
            if (Physics.Linecast(_guard.transform.position, _player.transform.position, _guard.VisionLayerMask))
            {
                Debug.DrawLine(_guard.transform.position, _player.transform.position, Color.red);
                return false;
            }
            
            // We can see the player
            Debug.DrawLine(_guard.transform.position, _player.transform.position, Color.green);
            return true;
        }
    }
}