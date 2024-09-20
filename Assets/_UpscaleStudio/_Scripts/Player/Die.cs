using _UpscaleStudio._Scripts.Screens;
using _UpscaleStudio._Scripts.System.Handlers;
using UnityEngine;

namespace _UpscaleStudio._Scripts.Player {
    public class Die : MonoBehaviour {
        [SerializeField] private PlayerHandle player;

        public void HandleDeath() {
            
           
            SoundHandler.Instance.StopAllSounds();
            player.playerCamera.DisableRotation();
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ScreenHandle.instance.ShowScreen("GameOverScreen");
            Debug.Log("Player has died!");
        }
    }
}