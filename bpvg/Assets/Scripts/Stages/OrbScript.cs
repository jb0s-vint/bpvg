using Jake.System;
using UnityEngine;

namespace Jake.Stages
{
    public class OrbScript : MonoBehaviour
    {
        public GameObject PickupEffectsPrefab;
        
        // Constants
        private const float PICKUP_RADIUS = 0.5f;
        
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
                
                // Spawn pickup effects
                Instantiate(PickupEffectsPrefab, transform.position + new Vector3(0.0f, 1.25f, 0.0f), Quaternion.identity);
                
                // Self destruct
                Destroy(gameObject);
            }
        }
    }
}