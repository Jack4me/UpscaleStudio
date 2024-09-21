using _UpscaleStudio._Scripts.System.Handlers;
using UnityEngine;

namespace _UpscaleStudio._Scripts.Player {
    public class CameraController : MonoBehaviour {
        public float sensitivity = 2.0f;
        public float maxYAngle = 80.0f; 
        private bool canRotate = true;
        private float rotationX;

        private void Update() {
            if (GameHandler.Instance.IsPaused() || !canRotate) {
                return; 
            }

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

           
            transform.parent.Rotate(Vector3.up * mouseX * sensitivity);

          
            rotationX -= mouseY * sensitivity;
            rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
            transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);
        }
        public void DisableRotation() {
            canRotate = false;
        }

        // Метод для включения вращения камеры
        public void EnableRotation() {
            canRotate = true;
        }
    }
}