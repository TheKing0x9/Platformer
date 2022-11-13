using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Platformer.Managers;

namespace Platformer.UI {
    public class MainMenu : MonoBehaviour {
        
        [SerializeField] private Image m_volumeImage;
        [SerializeField] private Sprite m_volumeOn;
        [SerializeField] private Sprite m_volumeOff;

        private bool volumeOn = true;

        private void Awake() {
            SetVolumeSprite();
        }    

        public void OnPlayClicked() {
            SceneTransitionManager.instance.LoadLevel("Level1");
        }

        public void OnCreditsClicked() {
            SceneTransitionManager.instance.LoadLevel("Credits");
        }

        public void OnVolumeClicked() {
            volumeOn = !volumeOn;
            SetVolumeSprite();
        }

        private void SetVolumeSprite() {
            m_volumeImage.sprite = volumeOn ? m_volumeOn : m_volumeOff;
        }

    }
}
