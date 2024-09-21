using System.Collections.Generic;
using _UpscaleStudio._Scripts.System.Handlers;
using CodeBase.ScreenConfigData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _UpscaleStudio._Scripts.UI {
    public class ScreenHandle : MonoBehaviour {
        public static ScreenHandle instance; // Singleton instance
        [SerializeField] private ScreenConfig[] screenConfigs; // Array of screen configurations
        private Stack<GameObject> screenHistory = new Stack<GameObject>(); // History stack of opened screens
        private Dictionary<string, GameObject> screenRegistry = new Dictionary<string, GameObject>(); // Dictionary to store screens by name
        private bool isPauseScreenActive = false; // Boolean to track pause screen state
        private GameObject HUDInstance; // HUD (Heads-Up Display) instance

        private void Awake() {
            // Implement singleton pattern to ensure only one instance
            if (instance != null) {
                Destroy(gameObject); // Destroy duplicate instance
            } else {
                instance = this;
                DontDestroyOnLoad(gameObject); // Persist this object between scenes
            }

            LoadHUD(); // Load the HUD UI
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene loaded event
        }

        private void Update() {
            ActivatePause(); // Handle pause screen activation
        }

        private void OnDestroy() {
            SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from scene loaded event
        }

        // Method to activate or deactivate the pause screen
        private void ActivatePause() {
            if (GameHandler.Instance.IsPaused() && !isPauseScreenActive) {
                ShowScreen("PauseScreen"); // Show the pause screen when the game is paused
                isPauseScreenActive = true;
            } else if (!GameHandler.Instance.IsPaused() && isPauseScreenActive) {
                GoBack(); // Go back to the previous screen when unpaused
                // Reactivate the Loot and Timer screens after unpausing
                GameObject screenLoot = screenRegistry["Loot"];
                screenLoot.SetActive(true);
                GameObject screenTimer = screenRegistry["Timer"];
                screenTimer.SetActive(true);
                isPauseScreenActive = false;
            }
        }

        // Event handler to load screens when a scene is loaded
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            LoadScreensForScene(scene.name); // Load the screens for the new scene
        }

        // Method to load the HUD (Heads-Up Display)
        private void LoadHUD() {
            if (HUDInstance == null) {
                GameObject hudPrefab = Resources.Load<GameObject>("UI/HUD/HUD"); // Load HUD prefab from Resources
                HUDInstance = Instantiate(hudPrefab); // Instantiate the HUD object
                DontDestroyOnLoad(HUDInstance); // Keep the HUD across scenes
            }
        }

        // Method to load screens for the current scene
        private void LoadScreensForScene(string sceneName) {
            RemoveOldScreens(); // Remove screens from the previous scene
            foreach (var config in screenConfigs) {
                if (config.SceneName == sceneName) {
                    InitializeScreens(config.screens); // Initialize screens for the current scene
                    return;
                }
            }
            Debug.LogError("Screen configuration for scene " + sceneName + " not found!");
        }

        // Method to remove all screens from the previous scene
        private void RemoveOldScreens() {
            foreach (var screen in screenRegistry.Values) {
                Destroy(screen); // Destroy the screen objects
            }
            screenRegistry.Clear(); // Clear the screen registry
            screenHistory.Clear(); // Clear the screen history
        }

        // Method to initialize screens for the current scene
        private void InitializeScreens(ScreenConfig.ScreenInfo[] screensInfo) {
            screenHistory.Clear(); // Clear any existing screen history
            screenRegistry.Clear(); // Clear the screen registry

            // Loop through and initialize each screen in the scene
            foreach (ScreenConfig.ScreenInfo screenInfo in screensInfo) {
                GameObject screen = Instantiate(screenInfo.screenPrefab, HUDInstance.transform); // Instantiate screen

                RegisterScreen(screen.name, screen); // Register the screen in the dictionary

                // Set screen active state based on its default setting
                screen.SetActive(screenInfo.isActiveByDefault);

                // If the screen is active by default, add it to the history stack
                if (screenInfo.isActiveByDefault) {
                    screenHistory.Push(screen);
                }
            }
        }

        // Method to show a specific screen by name
        public void ShowScreen(string screenName) {
            if (screenRegistry.ContainsKey(screenName)) {
                HideAllScreens(); // Hide all other screens
                GameObject screenToOpen = screenRegistry[screenName];
                screenToOpen.SetActive(true); // Activate the screen to open

                // Deactivate the current screen if one is active
                if (screenHistory.Count > 0) {
                    screenHistory.Peek().SetActive(false);
                }
                screenHistory.Push(screenToOpen); // Push the opened screen onto the history stack
            } else {
                Debug.LogError("Screen not found: " + screenName);
            }
        }

        // Method to go back to the previous screen
        public void GoBack() {
            if (screenHistory.Count > 1) {
                screenHistory.Pop().SetActive(false); // Deactivate the current screen
                screenHistory.Peek().SetActive(true); // Reactivate the previous screen
            }
        }

        // Method to register a screen in the registry
        public void RegisterScreen(string screenName, GameObject screenObject) {
            if (!screenRegistry.ContainsKey(screenName)) {
                screenRegistry.Add(screenName, screenObject); // Add screen to the registry
            }
        }

        // Method to hide all screens
        private void HideAllScreens() {
            foreach (var screen in screenRegistry.Values) {
                screen.SetActive(false); // Deactivate each screen
            }
        }
    }
}
