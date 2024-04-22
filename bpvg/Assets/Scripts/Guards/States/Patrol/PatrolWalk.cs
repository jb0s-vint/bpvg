namespace Jake.Guards.States.Patrol
{
    public class PatrolWalk : WatchingState
    {
        public PatrolWalk(GuardScript guard) : base(guard)
        {
        }

        public override void OnEntered()
        {
            // Determine where to walk
            var target = _guard.DecidePatrolTarget();
            
            // Set speed values
            _guard.NavMeshAgent.speed = 1.00f;
            _guard.Animation.CrossFade(_guard.WalkAnim);
            
            // Walk there
            _guard.NavMeshAgent.SetDestination(target);
        }

        public override void OnExited()
        {
            // Stop in our tracks
            _guard.NavMeshAgent.SetDestination(_guard.transform.position);
        }

        public override void Update()
        {
            base.Update();
            
            if (_guard.NavMeshAgent.remainingDistance > _guard.NavMeshAgent.stoppingDistance) return;
            _guard.SetState(new PatrolIdle(_guard));
        }
    }
}