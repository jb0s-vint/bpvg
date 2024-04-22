using Jake.Guards.States.Patrol;
using Jake.Player;
using Jake.System;
using UnityEngine;
using UnityEngine.AI;

namespace Jake.Guards
{
    public class GuardScript : HaltableBehavior
    { 
        [Header("AI")]
        public NavMeshAgent NavMeshAgent;
        [Range(0, 100)] public float Awareness;
        public LayerMask VisionLayerMask;
        
        [Header("Animation")]
        public Animation Animation;
        public string IdleAnim;
        public string WalkAnim;
        public string RunAnim;

        // State Machine
        public GuardState CurrentState { get; private set; }

        // References
        private PlayerControlScript _player;

        private void Start()
        {
            _player = FindObjectOfType<PlayerControlScript>();
            SetState(new PatrolIdle(this));
        }
        
        public override void UnhaltedUpdate()
            => CurrentState?.Tick();

        public override void Halted()
        {
            NavMeshAgent.isStopped = true;
            Animation.CrossFade(IdleAnim);
        }

        public override void Resumed()
        {
            NavMeshAgent.isStopped = false;
        }

        public void SetState(GuardState state)
        {
            CurrentState?.OnExited();
            CurrentState = state;
            CurrentState.OnEntered();
            
            Debug.Log("New state: " + state.GetType().Name);
        }

        /// <summary>
        /// Decides a random patrol target.
        /// This location will increasingly grow closer to the player's location based on Awareness.
        /// </summary>
        /// <returns>Location to walk to.</returns>
        public Vector3 DecidePatrolTarget()
        {
            var playerPos = _player.transform.position;
            var randomTarget = new Vector3(Random.Range(-21.0f, 21.0f), 0.0f, Random.Range(-21.0f, 21.0f));
            
            // Use linear interpolation between the random target and the player's position
            // to move the target closer to the player based on the level of awareness.
            return Vector3.Lerp(randomTarget, playerPos, Awareness / 100.0f);
        }
    }
}