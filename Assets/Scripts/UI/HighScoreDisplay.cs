using UnityEngine;
using UnityEngine.UI;

using Platformer.Data;

namespace Platformer.UI {
    public class HighScoreDisplay : MonoBehaviour {
        [SerializeField] private LevelData m_levelData;

        private Text m_text;

        private void Awake() {
            m_text = GetComponent<Text>();
        }

        private void Start() {
            m_text.text = "High Score : " + m_levelData.GetCurrentHighScore().ToString() + " Pts";
        }
    }
}