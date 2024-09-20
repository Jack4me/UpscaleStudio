using System;
using System.Collections.Generic;
using CodeBase.ScreenConfigData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _UpscaleStudio._Scripts.Screens {
    public class ScreenHandle : MonoBehaviour {
        public static ScreenHandle instance;
        [SerializeField] private ScreenConfig[] screenConfigs;
        private Stack<GameObject> screenHistory = new Stack<GameObject>();
        private Dictionary<string, GameObject> screenRegistry = new Dictionary<string, GameObject>();
        private bool isPauseScreenActive = false; 
        private GameObject HUDInstance;

        private void Awake() {
            if (instance != null) {
                Destroy(gameObject);
            }
            else {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            LoadHUD();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Update() {
            ActivatePause();
        }

        private void OnDestroy() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void ActivatePause() {
            if (GameHandler.Instance.IsPaused() && !isPauseScreenActive) {
                ShowScreen("PauseScreen");
                isPauseScreenActive = true;
            }

            else if (!GameHandler.Instance.IsPaused() && isPauseScreenActive) {
                GoBack();
                GameObject screenLoot = screenRegistry["Loot"];
                screenLoot.SetActive(true);
                GameObject screenTimer = screenRegistry["Timer"];
                screenTimer.SetActive(true);
                isPauseScreenActive = false;
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            LoadScreensForScene(scene.name);
        }

        private void LoadHUD() {
            if (HUDInstance == null) {
                GameObject hudPrefab = Resources.Load<GameObject>("UI/HUD/HUD");
                HUDInstance = Instantiate(hudPrefab);
                DontDestroyOnLoad(HUDInstance);
            }
        }

        private void LoadScreensForScene(string sceneName) {
            RemoveOldScreens();
            foreach (var config in screenConfigs) {
                if (config.SceneName == sceneName) {
                    InitializeScreens(config.screens);
                    return;
                }
            }

            Debug.LogError("Конфигурация экранов для сцены " + sceneName + " не найдена!");
        }

        private void RemoveOldScreens() {
            // Удаляем все экраны предыдущей сцены
            foreach (var screen in screenRegistry.Values) {
                Destroy(screen); // Удаляем объект экрана из сцены
            }

            screenRegistry.Clear(); // Очищаем словарь
            screenHistory.Clear();
        }

        private void InitializeScreens(ScreenConfig.ScreenInfo[] screensInfo) {
            screenHistory.Clear(); // На всякий случай очищаем историю
            screenRegistry.Clear(); // Очищаем реестр

            // Инициализация экранов для данной сцены
            foreach (ScreenConfig.ScreenInfo screenInfo in screensInfo) {
                GameObject screen = Instantiate(screenInfo.screenPrefab, HUDInstance.transform);

                // Регистрация экрана
                RegisterScreen(screen.name, screen); // Регистрируем экран в словаре

                // Устанавливаем активность по умолчанию
                screen.SetActive(screenInfo.isActiveByDefault);

                // Если экран активен по умолчанию, добавляем его в историю
                if (screenInfo.isActiveByDefault) {
                    screenHistory.Push(screen);
                }
            }
        }


        public void ShowScreen(string screenName) {
            if (screenRegistry.ContainsKey(screenName)) {
                HideAllScreens();
                GameObject screenToOpen = screenRegistry[screenName];
                screenToOpen.SetActive(true);
                if (screenHistory.Count > 0) {
                    screenHistory.Peek().SetActive(false);
                }
                screenHistory.Push(screenToOpen);
            }
            else {
                Debug.LogError("Screen not found: " + screenName);
            }
        }

        public void GoBack() {
            if (screenHistory.Count > 1) {
                screenHistory.Pop().SetActive(false);
                screenHistory.Peek().SetActive(true);
            }
        }

        public void RegisterScreen(string screenName, GameObject screenObject) {
            if (!screenRegistry.ContainsKey(screenName)) {
                screenRegistry.Add(screenName, screenObject);
            }
        }

        private void HideAllScreens() {
            foreach (var screen in screenRegistry.Values) {
                screen.SetActive(false);
            }
        }
    }
}