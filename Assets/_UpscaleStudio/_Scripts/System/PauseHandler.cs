using UnityEngine;

namespace _UpscaleStudio._Scripts.System {
    public class PauseHandler : MonoBehaviour {
        private bool isPaused = false;

        // Pause Method
        public void PauseGame() {
            if (!isPaused) {
                Time.timeScale = 0f;
                isPaused = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Debug.Log("Game Paused");
            }
        }


        public void ResumeGame() {
            if (isPaused) {
                Time.timeScale = 1f;
                isPaused = false;
                Debug.Log("Game Resumed");
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        // Method switches between pause and resume game
        public void TogglePause() {
            if (isPaused) {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }

        public bool IsPaused() {
            return isPaused;
        }
    }
}