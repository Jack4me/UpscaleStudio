using UnityEngine;

namespace _UpscaleStudio._Scripts.Data.SoundData {
    [CreateAssetMenu(fileName = "NewSound", menuName = "Audio/Sound")]
    public class SoundData : ScriptableObject {
        public AudioClip audioClip;
        public float volume = 1f;
        public bool loop;
        public bool is3D = false;
    }
}