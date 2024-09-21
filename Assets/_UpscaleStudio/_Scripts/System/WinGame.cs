using System.Collections;
using _UpscaleStudio._Scripts.Items;
using _UpscaleStudio._Scripts.Player;
using _UpscaleStudio._Scripts.System.Handlers;
using _UpscaleStudio._Scripts.UI;
using UnityEngine;

namespace _UpscaleStudio._Scripts.System {
    public class WinGame : MonoBehaviour {
        [SerializeField] private LootHandle lootHandle;
        [SerializeField] private PlayerHandle player;
        [SerializeField] public ParticleSystem particleEffect;
        private int keysCount;

        private IEnumerator InitializeLootHandleWithDelay() {
            yield return new WaitForEndOfFrame();  // Wait for the frame to complete
            lootHandle = LootHandle.Instance;

            if (lootHandle != null) {
                lootHandle.ChangedAction += UpdateKeysCount; // Subscribing once during initialization
            } else {
                Debug.LogError("LootHandle instance is missing!");
            }
        }

        private void Start() {
            StartCoroutine(InitializeLootHandleWithDelay());  // Delayed initialization to ensure LootHandle is set
        }

        private void OnEnable() {
            // Subscribing during enabling if lootHandle is already assigned
            if (lootHandle != null) {
                lootHandle.ChangedAction += UpdateKeysCount;
            }
        }

        private void OnDisable() {
            // Safely unsubscribe from the event to avoid memory leaks
            if (lootHandle != null) {
                lootHandle.ChangedAction -= UpdateKeysCount;
            }
        }

        // Updates the keys count based on lootHandle's collected value
        private void UpdateKeysCount() {
            keysCount = lootHandle.Collected;
            if (keysCount == 3) {
                particleEffect.Play();

            }
        }

        // Trigger the win condition when player enters the collider
        private void OnTriggerEnter(Collider other) {
            if (keysCount >= 3) { 
                // Check if the player has collected enough keys
                if (other.TryGetComponent(out PlayerHandle playerHandle)) {
                    Time.timeScale = 0f;  // Stop the game
                    Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
                    Cursor.visible = true;  // Make cursor visible
                    ScreenHandle.instance.ShowScreen("WinScreen");  // Show the win screen
                    Debug.Log("YOU WIN");  // Log the win
                    SoundHandler.Instance.StopAllSounds();  // Stop all game sounds
                    player.playerCamera.DisableRotation();  // Disable player camera rotation
                }
            }
        }
    }
}
