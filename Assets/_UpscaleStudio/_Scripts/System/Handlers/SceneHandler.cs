using System;

namespace _UpscaleStudio._Scripts.System.Handlers {
    public static class SceneHandler {
        public enum SceneType { 
            Loading,
            MainMenu,
            Preferences,
            FirstLevel,
        }
        
        private static SceneType _previousSceneType; 
        private static Action onLoaderCallBack; 

        // Method to load a new scene
        // 'sceneType' is the type of the scene to load
        // 'useLoadingScreen' determines whether to show the loading screen (default is true)
        public static void Load(SceneType sceneType, bool useLoadingScreen = true) {
            // Record the current scene as the previous scene
            _previousSceneType = (SceneType)Enum.Parse(typeof(SceneType), UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            
            onLoaderCallBack = () => {
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneType.ToString());
            };

            if (useLoadingScreen) {
                UnityEngine.SceneManagement.SceneManager.LoadScene(SceneType.Loading.ToString());
            } else {
                LoaderCallBack(); 
            }
        }

        // Callback to load the actual scene after the loading screen finishes
        public static void LoaderCallBack() {
            if (onLoaderCallBack != null) {
                onLoaderCallBack(); // Invoke the callback to load the new scene
                onLoaderCallBack = null; // Reset the callback to avoid repeated calls
            }
        }

        // Method to load the previous scene
        // 'useLoadingScreen' determines whether to show the loading screen (default is true)
        public static void LoadPreviousScene(bool useLoadingScreen = true) {
            Load(_previousSceneType, useLoadingScreen); 
        }
    }
}
