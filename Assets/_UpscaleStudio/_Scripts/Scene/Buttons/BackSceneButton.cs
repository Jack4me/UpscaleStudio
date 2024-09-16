using UnityEngine;
using UnityEngine.UI;
using UpscaleStudio._Scripts.Player.Screen.Scene;

namespace CodeBase.ScreenManager {
    public class BackButton : MonoBehaviour
    {
        [SerializeField] private Button backButton; 

        private void Start() {
            backButton.onClick.AddListener(OnBackButtonClicked);
        }

        private void OnBackButtonClicked() {
            SceneHandler.LoadPreviousScene(useLoadingScreen: false); 
        }
    
    
    }
}
