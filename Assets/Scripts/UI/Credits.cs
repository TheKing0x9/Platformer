using UnityEngine;
using System.Collections;

using Platformer.Managers;

namespace Platformer.UI {
    public class Credits : MonoBehaviour {
        private void Start() {
            StartCoroutine(TimeOut());
        }

        IEnumerator TimeOut() {
            yield return new WaitForSeconds(6f);

            SceneTransitionManager.instance.LoadLevel("Menu");
        }
    }
}