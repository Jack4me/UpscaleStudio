using System;
using TMPro;
using UnityEngine;

namespace _UpscaleStudio._Scripts.Items {
    public class LootCounter : MonoBehaviour {
        public TextMeshProUGUI CounterText;

        private void Awake() {
            LootManager.Instance.ChangedAction += UpdateCounterText;
        }


        private void Start() {
            UpdateCounterText();
        }

        private void UpdateCounterText() { //CounterText.text = $"{LootManager.Instance.Collected}";
            CounterText.text = $"{LootManager.Instance.Collected}";
        }
    }
}