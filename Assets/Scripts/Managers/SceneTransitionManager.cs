using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

using Platformer.Utilities;
using Platformer.Data;

namespace Platformer.Managers {
    public class SceneTransitionManager : PersistentSingleton<SceneTransitionManager> {

        [SerializeField] private Animator m_animator;
        [SerializeField] private LevelData m_levelData;

        private int fadeParamID;

        protected override void Awake() {
            base.Awake();

            m_levelData.currentLevel = 0;
            fadeParamID = Animator.StringToHash("Fade");
            SceneManager.activeSceneChanged += MoveIn;
        }

        public void ReloadLevel() {
            LoadLevel(SceneManager.GetActiveScene().name);
        }

        public void LoadNextLevel() {
            int nextLevel = ++m_levelData.currentLevel;
            LoadLevel(m_levelData[nextLevel].sceneName);
        }

        public void LoadLevel(string nextLevel) {
            StartCoroutine(MoveIn(nextLevel));
        }

        private IEnumerator MoveIn(string nextLevel) {
            m_animator.SetBool(fadeParamID, false);
            yield return new WaitForSeconds(0.6f);
            SceneManager.LoadScene(nextLevel);
        }
        
        private void MoveIn(Scene prev, Scene next) {
            m_animator.SetBool(fadeParamID, true);
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            SceneManager.activeSceneChanged -= MoveIn;
        }
    }
}