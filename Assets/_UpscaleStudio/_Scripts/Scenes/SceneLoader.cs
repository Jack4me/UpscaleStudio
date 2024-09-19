using UnityEngine;
using UpscaleStudio._Scripts.Player.Screen.Scene;

namespace _UpscaleStudio._Scripts.Scenes {
    public class SceneLoader : MonoBehaviour {
        
        public void LoadScene(SceneHandler.SceneType scene, bool useLoadingScreen = true) {
            SceneHandler.Load(scene, useLoadingScreen);
        }
    }
}