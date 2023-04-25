using UnityEngine;
using UnityEngine.SceneManagement;

using Platformer.Utilities;
using Platformer.Data;
using System;

namespace Platformer.Audio {
    public class MusicManager : PersistentSingleton<MusicManager> {
        [Header("Music Parameters")]
		[SerializeField] private float m_startDelay = 0.5f;
		[SerializeField] private float m_fadeRate = 2f;
        [SerializeField] private LevelData m_levelData; 


        [Header("Other Levels")]

        [UDictionary.Split(50, 50)]
        [SerializeField] private LevelDictionary m_otherLevels;

        [System.Serializable] public class LevelDictionary : UDictionary<string, AudioClip> {} ;

		private float m_originalVolume;
		private float m_fadeLevel = 1f;
        private AudioSource m_musicSource;

        private void Start() {
            m_musicSource = GetComponent<AudioSource> ();
            SceneManager.activeSceneChanged += OnSceneChanged;

            m_originalVolume = m_musicSource.volume;
            PlayMusic(m_otherLevels["Menu"]);
        }

        public void StopMusic() {
            m_musicSource.Stop();
        }

        public void PlayCurrentMusic() {
            m_musicSource.Play();
        }

        private void PlayMusic(AudioClip clip, bool fadeIn = true, bool loop = true) {
            if (m_musicSource.clip == clip) return;

            m_musicSource.Stop();

            m_musicSource.clip = clip;
            m_musicSource.loop = loop;
            m_musicSource.PlayDelayed(m_startDelay);

            if (fadeIn) m_fadeLevel = -m_startDelay;
        }

        private void OnSceneChanged(Scene oldScene, Scene newScene) {
            if (m_musicSource != null) m_musicSource.pitch = 1;

            try {
                PlayMusic(m_otherLevels[newScene.name]);
            } catch (Exception) {
                PlayMusic(m_levelData[m_levelData.currentLevel].levelMusic);
            }
        }

        private void Update() {
            if (m_fadeLevel < 1f && m_fadeRate > 0f) {
                m_fadeLevel = Mathf.Lerp(m_fadeLevel, 1f, Time.deltaTime * m_fadeRate);

                if (m_fadeLevel > 0.99f) m_fadeLevel = 1f;
                m_musicSource.volume = m_originalVolume * m_fadeLevel;
            }
        }

        protected override void OnDestroy() {
            SceneManager.activeSceneChanged -= OnSceneChanged;
            base.OnDestroy();
        }
    }
}
