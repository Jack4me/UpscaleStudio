using System;
using _UpscaleStudio._Scripts.Items;
using UnityEngine;

namespace _UpscaleStudio._Scripts {
    public class LootManager : MonoBehaviour
    {
        public int Collected;
        public Action ChangedAction;

        public static LootManager Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }

        public void Collect(LootPiece loot){
            Collected += loot.countKey;
            ChangedAction?.Invoke();
        }

        
    }
}
