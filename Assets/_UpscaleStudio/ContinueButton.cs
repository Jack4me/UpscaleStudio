using UnityEngine;
using UnityEngine.UI;

namespace _UpscaleStudio {
    public class ContinueButton : MonoBehaviour
    {

        [SerializeField] private Button continueButton;

        private void Start() {
            continueButton.onClick.AddListener(OnContinueGameButtonClick);
        }

        private void OnContinueGameButtonClick() {
            throw new System.NotImplementedException();
        }
    }
}
