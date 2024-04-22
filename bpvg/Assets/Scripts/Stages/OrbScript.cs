using Jake.Player;
using Jake.System;
using UnityEngine;

namespace Jake.Stages
{
    public class OrbScript : MonoBehaviour
    {
        // Constants
        private const float PICKUP_RADIUS = 0.4f;
        
        // References
        private PlayerControlScript _player;

        private void Start()
        {
            _player = FindObjectOfType<PlayerControlScript>();
        }

        private void Update()
        {
            var dist = Vector3.Distance(transform.position, _player.transform.position);
            
            // Is the player within pickup distance of the orb?
            if (dist < PICKUP_RADIUS)
            {
                // Increase score
                GameManager.Instance.OrbCollected();
                
                // Self destruct
                Destroy(gameObject);
            }
        }
    }
}