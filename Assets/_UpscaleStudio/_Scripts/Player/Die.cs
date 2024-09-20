using _UpscaleStudio._Scripts.Screens;
using UnityEngine;

namespace _UpscaleStudio._Scripts.Player {
    public class Die : MonoBehaviour {
        public void HandleDeath() {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ScreenHandle.instance.ShowScreen("GameOverScreen");
            Debug.Log("Player has died!");
        }
    }
}