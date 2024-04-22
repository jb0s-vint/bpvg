using UnityEngine;

namespace Jake.System
{
    public class CharacterScript : MonoBehaviour
    {
        [Header("~ Audio")]
        [SerializeField] private AudioSource _soundSource;
        [SerializeField] private AudioClip[] _footstepClips;
        [SerializeField] private AudioClip _alertClip;

        [Header("~ Particles")]
        [SerializeField] private ParticleSystem _heardParticle;
        [SerializeField] private ParticleSystem _seenParticle;

        /// <summary>
        /// Plays a question effect at the character's location.
        /// </summary>
        public void Question()
        {
            _seenParticle.Stop();
            _heardParticle.Play();
        }
        
        /// <summary>
        /// Plays an alert effect at the character's location.
        /// </summary>
        public void Alert()
        {
            _soundSource.PlayOneShot(_alertClip);
            _heardParticle.Stop();
            _seenParticle.Play();
        }
        
        /// <summary>
        /// Plays a footstep sound at the character's location.
        /// </summary>
        public void Footstep()
            => _soundSource.PlayOneShot(_footstepClips[Random.Range(0, _footstepClips.Length)], 1.0f);
    }
}