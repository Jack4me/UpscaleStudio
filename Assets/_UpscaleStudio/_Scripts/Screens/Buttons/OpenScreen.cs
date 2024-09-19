using _UpscaleStudio._Scripts.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace UpscaleStudio._Scripts.Player.Screen {
    public class OpenScreen : MonoBehaviour {
        [SerializeField] private string screenName;
        [SerializeField] private Button OpenWindowButton;

        private void Start() {
            OpenWindowButton.onClick.AddListener(OnOpenHUDButtonClick);
        }

        private void OnOpenHUDButtonClick() {
            ScreenHandle.instance.ShowScreen(screenName);
        }
    }
}