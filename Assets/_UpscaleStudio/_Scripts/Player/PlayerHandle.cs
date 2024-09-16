using UnityEngine;

namespace UpscaleStudio._Scripts.Player {
    public class PlayerHandle : MonoBehaviour {
        [SerializeField] public float moveSpeed = 5.0f; // Скорость движения персонажа
        [SerializeField] public float sprintSpeed = 10.0f;

        private CharacterController controller;
        private float currentSpeed;

        private void Start() {
            controller = GetComponent<CharacterController>();
            currentSpeed = moveSpeed;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update() {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");

            var moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

            moveDirection.y -= 9.81f * 5 * Time.deltaTime;

            if (Input.GetKey(KeyCode.LeftShift))
                currentSpeed = sprintSpeed;
            else
                currentSpeed = moveSpeed;

            controller.Move(moveDirection * currentSpeed * Time.deltaTime);
        }
    }
}