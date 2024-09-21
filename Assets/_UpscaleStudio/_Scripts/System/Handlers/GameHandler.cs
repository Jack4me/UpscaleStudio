using UnityEngine;
using UnityEngine.SceneManagement;

namespace _UpscaleStudio._Scripts.System.Handlers {
    public class GameHandler : MonoBehaviour {
        [SerializeField] private PauseHandler pauseHandler; // Reference to the PauseHandler
        public static GameHandler Instance; // Singleton instance

        // Awake method to implement the Singleton pattern
        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject); // Destroy duplicate instance
            } else {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Keep GameHandler across scenes
            }
        }

        // Update method to handle pause toggle
        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape) && IsPauseScene()) {
                pauseHandler.TogglePause(); // Toggle pause when "Escape" key is pressed in the correct scene
            }
        }

        // Method to check if the current scene supports pause functionality
        public bool IsPauseScene() {
            string currentScene = SceneManager.GetActiveScene().name;

            // Specify which scenes should support pausing
            return currentScene == "FirstLevel"; // Replace with the scene where pause is allowed
        }

        // Method to check if the game is currently paused
        public bool IsPaused() {
            if (pauseHandler != null) {
                return pauseHandler.IsPaused(); // Return pause status from PauseHandler
            }

            return false;
        }

        // Getter to access the PauseHandler instance
    


        public PauseHandler GetPause() {
            return pauseHandler;
        }
    }
}