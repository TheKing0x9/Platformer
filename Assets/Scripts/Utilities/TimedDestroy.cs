using UnityEngine;

namespace Platformer.Utilities {
    public class TimedDestroy : MonoBehaviour {
        [SerializeField] private float m_destroyTime = 2f;
        [SerializeField] private bool m_destroyChildren = false;

        private void Start() {
            Destroy(gameObject, m_destroyTime);
        }

        private void OnDestroy() {
            if (!m_destroyChildren) return;

            for (int i = 0; i < transform.childCount; i++) {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}