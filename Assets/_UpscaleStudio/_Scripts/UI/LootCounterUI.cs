using System;
using TMPro;
using UnityEngine;

namespace _UpscaleStudio._Scripts.Items {
    public class LootCounterUI : MonoBehaviour {
        public TextMeshProUGUI CounterText;

        private void Awake() {
            LootHandle.Instance.ChangedAction += UpdateCounterText;
        }


        private void Start() {
            UpdateCounterText();
        }
     

        private void UpdateCounterText() { 
            string collectedText = LootHandle.Instance.Collected.ToString();
            CounterText.text = $"{collectedText}/3";
        }
    }
}