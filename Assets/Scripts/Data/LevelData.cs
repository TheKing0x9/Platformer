using UnityEngine;
using System.Collections.Generic;

namespace Platformer.Data {
    [System.Serializable]
    public class LevelDetail {
        public string sceneName;
        public int highScore;
        public AudioClip levelMusic;
    }

    [CreateAssetMenu(fileName = "LevelData", menuName = "Platformer/LevelData")]
    public class LevelData : ScriptableObject {
        [ReadOnly] public int currentLevel;
        
        [SerializeField] private List<LevelDetail> m_levels;

        public LevelDetail this [int index] { get => m_levels[index]; }
    }
}   
