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
            HandleMovement(); // Process player movement each frame
        }

        private void OnTriggerEnter(Collider other) {
            // Handle player death if colliding with an enemy
            if (other.TryGetComponent(out Enemy.Enemy enemy)) {
                die.HandleDeath(); 
            }
        }

        private void InitializeComponents() {
            // Initialize the character controller and set the initial speed
            controller = GetComponent<CharacterController>();
            if (controller == null) {
                Debug.LogError("CharacterController is missing!");
            }
            currentSpeed = moveSpeed;
        }

        private void ConfigureCursor() {
            // Lock and hide the cursor during gameplay
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void HandleMovement() {
            // Calculate movement based on player input
            Vector3 moveDirection = GetInputMovement();
            bool wasMoving = isMoving; // Store previous movement state
            isMoving = moveDirection.magnitude > 0.1f; // Check if the player is moving

            ApplyGravity(ref moveDirection); // Apply gravity to the movement
            UpdateSpeed(); // Update the player's movement speed

            controller.Move(moveDirection * currentSpeed * Time.deltaTime); // Move the character

            HandleFootsteps(wasMoving); // Play or stop footstep sounds based on movement
        }

        private Vector3 GetInputMovement() {
            // Get player input for movement along the horizontal and vertical axes
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            return transform.forward * verticalInput + transform.right * horizontalInput;
        }

        private void ApplyGravity(ref Vector3 moveDirection) {
            // Apply gravity to the player's movement direction
            moveDirection.y -= Gravity * Time.deltaTime;
        }

        private void UpdateSpeed() {
            // Update movement speed based on whether the player is sprinting or walking
            if (Input.GetKey(KeyCode.LeftShift)) {
                currentSpeed = sprintSpeed; // Sprint speed
            } else {
                currentSpeed = moveSpeed; // Normal walking speed
            }
        }

        private void HandleFootsteps(bool wasMoving) {
            // Play or stop footstep sounds based on movement
            if (isMoving) {
                if (!wasMoving) {
                    // Start playing footstep sound when the player starts moving
                    SoundHandler.Instance.PlaySound(footstepsSound, transform.position, gameObject);
                }

                // Adjust footstep sound speed when sprinting
                if (Input.GetKey(KeyCode.LeftShift)) {
                    SoundHandler.Instance.SetSoundSpeed(gameObject, 1.5f); // Faster sound when sprinting
                } else {
                    SoundHandler.Instance.SetSoundSpeed(gameObject, 1.0f); // Normal sound speed
                }
            } else if (wasMoving) {
                // Stop footstep sound when the player stops moving
                SoundHandler.Instance.StopSound(footstepsSound, gameObject);
            }
        }
    }
}
