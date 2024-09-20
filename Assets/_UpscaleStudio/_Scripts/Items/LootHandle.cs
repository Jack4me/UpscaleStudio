using System;
using _UpscaleStudio._Scripts.Data.SoundData;
using _UpscaleStudio._Scripts.System.Handlers;
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
        public void ResetCollectedKeys() {
            Collected = 0; // Сбрасываем количество ключей
            ChangedAction?.Invoke(); // Обновляем UI или другие зависимости
        }
        public void Collect(LootPiece loot){
            Collected += loot.CountKey;
            ChangedAction?.Invoke();
            SoundHandler.Instance.PlaySound(keySound, transform.position, gameObject);
            
        }

        
    }
}
