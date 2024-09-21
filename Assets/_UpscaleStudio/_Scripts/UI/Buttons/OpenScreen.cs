using _UpscaleStudio._Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _UpscaleStudio._Scripts.Screens.Buttons {
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