using UnityEngine;
using TMPro;

using Platformer.Data;

namespace Platformer.UI {
    public class HighScoreDisplay : MonoBehaviour {
        [SerializeField] private LevelData m_levelData;

        private TextMeshPro m_mesh;

        private void Awake() {
            m_mesh = GetComponent<TextMeshPro>();
        }

        private void Start() {
            m_mesh.text = m_levelData.GetCurrentHighScore().ToString() + " Pts";
        }
    }
}