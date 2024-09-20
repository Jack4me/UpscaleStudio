using _UpscaleStudio._Scripts.Items;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _UpscaleStudio {
    public class RestartButton : MonoBehaviour {
        [SerializeField] private Button restartButton;

        void Start() {
            restartButton.onClick.AddListener(RestartGame);
        }


        public void RestartGame() {
            LootHandle.Instance.Collected = 0;
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}