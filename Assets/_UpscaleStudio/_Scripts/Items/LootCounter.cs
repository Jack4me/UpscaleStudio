using TMPro;
using UnityEngine;

namespace _UpscaleStudio._Scripts.Items {
    public class LootCounter : MonoBehaviour {
        private WorldData _worldData;
        public TextMeshProUGUI CounterText;

        public void Construct(WorldData worldData){
            _worldData = worldData;
            _worldData.LootData.ChangedAction += UpdateCounterText;
        }
            
        private void Start(){
            UpdateCounterText();
        }

        private void UpdateCounterText(){
            CounterText.text = $"{_worldData.LootData.Collected}";
        }
    }
}