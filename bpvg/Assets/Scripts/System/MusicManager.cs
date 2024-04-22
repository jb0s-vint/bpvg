using UnityEngine;

namespace Jake.System
{
    public class MusicManager : MonoBehaviour
    {
        public AudioSource AudioSource;

        /// <summary>
        /// Plays a music track once.
        /// </summary>
        /// <param name="clip">The music audioclip to play.</param>
        public void PlayOneShot(AudioClip clip)
        {
            AudioSource.clip = clip;
            AudioSource.loop = false;
            AudioSource.Play();
        }
        
        /// <summary>
        /// Plays a music track on loop.
        /// </summary>
        /// <param name="audioClip">The music audioclip to play.</param>
        public void PlayLoop(AudioClip audioClip)
        {
            AudioSource.clip = audioClip;
            AudioSource.loop = true;
            AudioSource.Play();
        }

        /// <summary>
        /// Stops any music currently playing.
        /// </summary>
        public void Stop()
        {
            AudioSource.clip = null;
            AudioSource.Stop();
        }
    }
}