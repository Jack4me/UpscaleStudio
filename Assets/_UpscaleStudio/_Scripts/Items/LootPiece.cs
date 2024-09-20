using UnityEngine;

namespace _UpscaleStudio._Scripts.Items {
    public class LootPiece : MonoBehaviour {
        [SerializeField] private int _countKey;

        public int CountKey
        {
            get { return _countKey; }
            private set { _countKey = value; }
        }

        public float rotationSpeed = 50f;

        void Update() {
            transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
        }

        private void OnTriggerEnter(Collider other) {
            PickUp();
        }

        private void PickUp() {
            Debug.Log("PickKey");
            UpdateUIData();
            Destroy(gameObject);
        }

        private void UpdateUIData() {
            LootHandle.Instance.Collect(this);
        }
    }
}