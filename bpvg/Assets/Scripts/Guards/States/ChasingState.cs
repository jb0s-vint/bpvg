using Jake.Guards.States.Patrol;
using Jake.System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jake.Guards.States
{
    public class ChasingState : WatchingState
    {
        // References
        private PlayerControlScript _player;
        
        public ChasingState(GuardScript guard) : base(guard)
        {
            _player = Object.FindObjectOfType<PlayerControlScript>();
        }

        public override void OnEntered()
        {
            Debug.Log($"{_guard.gameObject.name}: I spotted the player!");
            
            // Set speed values
            _guard.NavMeshAgent.speed = 5f;
            _guard.NavMeshAgent.isStopped = false;
            _guard.Animation.CrossFade(_guard.RunAnim);
            
            // Play alert sound
            _guard.Character.Alert();
        }

        public override void OnExited()
        {
            Debug.Log($"{_guard.gameObject.name}: I lost the player.");
        }

        public override void Update()
        {
            // Chase the player if we can see them
            if (CanSeePlayer())
            {
                // Increase speed
                _guard.NavMeshAgent.speed = Mathf.Clamp(_guard.NavMeshAgent.speed + Time.deltaTime / 10.0f, 5.0f, 10.0f);
                _guard.NavMeshAgent.SetDestination(_player.transform.position);
                
                // Did we catch the player?
                if (Vector3.Distance(_guard.transform.position, _player.transform.position) < 1.2f)
                {
                    // End the game -- you lost
                    GameManager.Instance.EndGame(false);
                }
                return;
            }
            
            // Decrease speed
            _guard.NavMeshAgent.speed = Mathf.Clamp(_guard.NavMeshAgent.speed - Time.deltaTime / 5.0f, 5.0f, 10.0f);
            
            // Did we arrive at the place we last saw the player?
            if (_guard.NavMeshAgent.remainingDistance <= _guard.NavMeshAgent.stoppingDistance)
                _guard.SetState(new PatrolIdle(_guard));
        }
    }
}