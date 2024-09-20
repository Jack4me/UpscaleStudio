using System;
using _UpscaleStudio._Scripts.Data.SoundData;
using UnityEngine;

namespace _UpscaleStudio._Scripts.Items {
    public class LootHandle : MonoBehaviour
    {
        public int Collected;
        public Action ChangedAction;
        [SerializeField] private SoundData keySound;

        public static LootHandle Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }

        public void Collect(LootPiece loot){
            Collected += loot.CountKey;
            ChangedAction?.Invoke();
            SoundHandler.Instance.PlaySound(keySound, transform.position, gameObject);
            
        }

        
    }
}
