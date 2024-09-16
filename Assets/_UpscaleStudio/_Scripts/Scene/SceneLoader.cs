using UnityEngine;

namespace UpscaleStudio._Scripts.Player.Screen.Scene {
    public class SceneLoader : MonoBehaviour {
        
        public void LoadScene(SceneHandler.SceneType scene, bool useLoadingScreen = true) {
            SceneHandler.Load(scene, useLoadingScreen);
        }
    }
}