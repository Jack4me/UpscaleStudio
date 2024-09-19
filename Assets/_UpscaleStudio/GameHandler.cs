using UnityEngine;
using UnityEngine.SceneManagement;

namespace _UpscaleStudio {
    public class GameHandler : MonoBehaviour {
        [SerializeField] PauseHandler pauseManager; // Ссылка на PauseManager
        public static GameHandler Instance;

        void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
            }
            else {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }


        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape) && IsPauseScene()) {
                pauseManager.TogglePause();
            }
        }

        // Method to check scene for Active Pause
        public bool IsPauseScene() {
            string currentScene = SceneManager.GetActiveScene().name;

            // Указываем, в каких сценах должна быть пауза
            return currentScene == "FirstLevel"; // Замени "SecondScene" на нужную сцену
        }

        public bool IsPaused() {
            if (pauseManager != null) {
                return pauseManager.IsPaused();
            }

            return false;
        }

        public PauseHandler GetReference() {
            return pauseManager;
        }
    }
}