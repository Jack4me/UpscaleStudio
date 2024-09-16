using UnityEngine;
using UnityEngine.UI;

namespace UpscaleStudio._Scripts.Player.Screen.Scene {
    public class OpenScene : MonoBehaviour {
        [SerializeField] private SceneHandler.SceneType sceneToLoad;
        [SerializeField] private Button openWindowButton;


   
        private SceneLoader sceneLoader;

        private void Start() {
            sceneLoader = FindObjectOfType<SceneLoader>();

            if (sceneLoader == null) {
                Debug.LogError("SceneLoader не найден на сцене!");
                return;
            }
            openWindowButton.onClick.AddListener(() => sceneLoader.LoadScene(sceneToLoad));
        }
    }
}