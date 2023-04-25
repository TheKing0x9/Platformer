using UnityEngine;

using Platformer.Utilities;

namespace Platformer.Audio {
    public class AudioManager : PersistentSingleton<AudioManager> {
        private AudioSource m_sfxSource;

        [SerializeField] private AudioClip m_jumpClip;
        [SerializeField] private AudioClip m_hitClip;
        [SerializeField] private AudioClip m_victoryClip;
        [SerializeField] private AudioClip m_pickupClip;

        private void Start() {
            m_sfxSource = GetComponent<AudioSource> ();

            if (m_sfxSource == null) {
                m_sfxSource =  this.gameObject.AddComponent<AudioSource> ();
            }
        }

        private void PlaySound(AudioClip clip) {
            m_sfxSource.clip = clip;
            m_sfxSource.PlayOneShot(clip);
        }

        public void PlayJumpSound() => PlaySound(m_jumpClip);
        public void PlayHitSound() => PlaySound(m_hitClip);
        public void PlayVictorySound() => PlaySound(m_victoryClip);
        public void PlayPickupSound() => PlaySound(m_pickupClip);
    }
}