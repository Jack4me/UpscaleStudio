using _UpscaleStudio._Scripts.Screens;
using UnityEngine;

namespace _UpscaleStudio {
    public class Die : MonoBehaviour {
        public void HandleDeath() {
            PauseHandler pause = GameHandler.Instance.GetPause();
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ScreenHandle.instance.ShowScreen("GameOverScreen");
            Debug.Log("Player has died!");
        }
    }
}