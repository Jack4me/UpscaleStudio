using _UpscaleStudio._Scripts.Data.SoundData;
using _UpscaleStudio._Scripts.System.Handlers;
using UnityEngine;

namespace _UpscaleStudio._Scripts.Player {
    public class PlayerHandle : MonoBehaviour {
        public CameraController playerCamera;

        [SerializeField] private float moveSpeed = 5.0f; 
        [SerializeField] private float sprintSpeed = 10.0f;
        [SerializeField] private Die die;
        [SerializeField] private SoundData footstepsSound;
        private CharacterController controller;
        private const float Gravity = 9.81f;
        private float currentSpeed;
        private bool isMoving = false;
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
            bool wasMoving = isMoving;
            isMoving = moveDirection.magnitude > 0.1f; 
            ApplyGravity(ref moveDirection);
            UpdateSpeed();

            controller.Move(moveDirection * currentSpeed * Time.deltaTime);
            
            HandleFootsteps(wasMoving);
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
        private void HandleFootsteps(bool wasMoving) {
            if (isMoving) {
                if (!wasMoving) {
                    SoundHandler.Instance.PlaySound(footstepsSound, transform.position, gameObject);
                }

                if (Input.GetKey(KeyCode.LeftShift)) {
                    SoundHandler.Instance.SetSoundSpeed(gameObject, 1.5f); 
                } else {
                    SoundHandler.Instance.SetSoundSpeed(gameObject, 1.0f); 
                }
            } else if (wasMoving) {
                SoundHandler.Instance.StopSound(footstepsSound, gameObject);
            }
        }
    }
}