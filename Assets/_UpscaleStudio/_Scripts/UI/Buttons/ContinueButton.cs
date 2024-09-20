using _UpscaleStudio._Scripts.System;
using UnityEngine;
using UnityEngine.UI;

namespace _UpscaleStudio {
    public class ContinueButton : MonoBehaviour {
        [SerializeField] private Button continueButton;

        private void Start() {
            continueButton.onClick.AddListener(OnContinueGameButtonClick);
        }

        private void OnContinueGameButtonClick() {
            PauseHandler pauseHandler = GameHandler.Instance.GetPause();
            pauseHandler.ResumeGame();
        }
    }
}