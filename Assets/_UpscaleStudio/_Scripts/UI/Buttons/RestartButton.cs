using _UpscaleStudio._Scripts.Items;
using _UpscaleStudio._Scripts.System.Handlers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _UpscaleStudio._Scripts.UI.Buttons {
    public class RestartButton : MonoBehaviour {
        [SerializeField] private Button restartButton;

        void Start() {
            restartButton.onClick.AddListener(RestartGame);
        }


        public void RestartGame() {
            LootHandle.Instance.Collected = 0;
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SoundHandler.Instance.PlayFirstLevelMusic();
        }
    }
}