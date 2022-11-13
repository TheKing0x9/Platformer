using UnityEngine;

using Platformer.Utilities;

namespace Platformer.Managers {
    public class GameManager : Singleton<GameManager> {
        public Event onScoreUpdated;

        [SerializeField] private GameObject m_player;
        public GameObject player { get => m_player; }

        private int m_score;
        
        public void PlayerDied() {
            SceneTransitionManager.instance.ReloadLevel();
        }

        public void LevelCompleted() {
            SceneTransitionManager.instance.LoadNextLevel();
        }

        public void AddScore(int score) {
            m_score += score;
            // onScoreUpdated();
        }
    }
}