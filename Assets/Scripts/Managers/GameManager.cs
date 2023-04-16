using UnityEngine;
using UnityEngine.UI;

using Platformer.Utilities;

namespace Platformer.Managers {
    public class GameManager : Singleton<GameManager> {
        [Header("Properties")]
        [SerializeField] private float m_scoreDecrementModifier = 20f;

        [Header("UI")]
        [SerializeField] private Text m_scoreText;
        [SerializeField] private Text m_highScoreText;

        [Header("References")]
        [SerializeField] private GameObject m_player;
        public GameObject player { get => m_player; }

        private float m_score = 1000f;
        private bool m_countdown = true;
        
        public void PlayerDied() {
            SceneTransitionManager.instance.ReloadLevel();
            m_countdown = false;
        }

        public void LevelCompleted() {
            SceneTransitionManager.instance.LoadNextLevel();
            m_countdown = false;
        }

        public void AddScore(int score) {
            m_score += score;
            
            UpdateScore();
        }

        private void UpdateScore() {
            if (m_scoreText) {
                m_scoreText.text = "Score : " + ((int)m_score).ToString() + " Pts";
            }
        }

        private void Update() {
            if(!m_countdown) return;

            m_score -= Time.deltaTime * m_scoreDecrementModifier;
            UpdateScore();

            if (m_score < 0) PlayerDied(); 
        }
    }
}