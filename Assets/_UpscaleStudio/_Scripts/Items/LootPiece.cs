using UnityEngine;

namespace _UpscaleStudio._Scripts.Items {
    public class LootPiece : MonoBehaviour {
        public int countKey { get; private set; }
        private bool _picked;

        private void OnTriggerEnter(Collider other){
            PickUp();
        }

        private void PickUp(){
            if (_picked){
                return;
            }
            _picked = true;
            UpdateUIData();
        }

        private void UpdateUIData() {
            LootManager.Instance.Collect(this);
        }
    }
}
