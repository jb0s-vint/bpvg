using UnityEngine;

namespace Jake.Guards.States.Patrol
{
    public class InvestigatingNoise : WatchingState
    {
        private Vector3 _location;
        
        public InvestigatingNoise(GuardScript guard, Vector3 location) : base(guard)
        {
            _location = location;
        }

        public override void OnEntered()
        {
            // Set speed values
            _guard.NavMeshAgent.speed = 1.00f;
            _guard.Animation.CrossFade(_guard.WalkAnim);
            
            // Walk there
            _guard.NavMeshAgent.SetDestination(_location);
            _guard.NavMeshAgent.isStopped = false;
        }

        public override void OnExited()
        {
            // Stop in our tracks
            _guard.NavMeshAgent.SetDestination(_guard.transform.position);
        }

        public override void Update()
        {
            base.Update();
            
            // When we arrive at the sound, go back to the normal patrol state.
            // This inherits WatchingState so in the case that the player is spotted, the chase will begin.
            if (_guard.NavMeshAgent.remainingDistance > _guard.NavMeshAgent.stoppingDistance) return;
            _guard.SetState(new PatrolIdle(_guard));
        }
    }
}