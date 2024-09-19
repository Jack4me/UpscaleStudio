using UnityEngine;

namespace _UpscaleStudio._Scripts.Player {
    public class PlayerHandle : MonoBehaviour {
        private const float Gravity = 9.81f;
        private const float SprintMultiplier = 2.0f;

        [SerializeField] private float moveSpeed = 5.0f; 
        [SerializeField] private float sprintSpeed = 10.0f;
        [SerializeField] private Die die;

        private CharacterController controller;
        private float currentSpeed;

        private void Start() {
            InitializeComponents();
            ConfigureCursor();
        }

        private void Update() {
            HandleMovement();
            
        }
        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out Enemy enemy)) {
                die.HandleDeath();
            }
        }
        private void InitializeComponents() {
            controller = GetComponent<CharacterController>();
            currentSpeed = moveSpeed;
        }

        private void ConfigureCursor() {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void HandleMovement() {
            Vector3 moveDirection = GetInputMovement();
            ApplyGravity(ref moveDirection);
            UpdateSpeed();

            controller.Move(moveDirection * currentSpeed * Time.deltaTime);
        }

        private Vector3 GetInputMovement() {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            return transform.forward * verticalInput + transform.right * horizontalInput;
        }

        private void ApplyGravity(ref Vector3 moveDirection) {
            moveDirection.y -= Gravity * Time.deltaTime;
        }

        private void UpdateSpeed() {
            if (Input.GetKey(KeyCode.LeftShift)) {
                currentSpeed = sprintSpeed;
            } else {
                currentSpeed = moveSpeed;
            }
        }
    }
}