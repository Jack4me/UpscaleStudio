using _UpscaleStudio._Scripts.Data.SoundData;
using _UpscaleStudio._Scripts.Screens;
using _UpscaleStudio._Scripts.System.Handlers;
using _UpscaleStudio._Scripts.UI;
using UnityEngine;

namespace _UpscaleStudio._Scripts.Player {
    public class Die : MonoBehaviour {
        [SerializeField] private PlayerHandle player;
        [SerializeField] private SoundData dyingSound;

        public void HandleDeath() {
            

            SoundHandler.Instance.StopAllSounds();
            player.playerCamera.DisableRotation();
            ScreenHandle.instance.ShowScreen("GameOverScreen");
            SoundHandler.Instance.PlaySound(dyingSound, transform.position, gameObject);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("Player has died!");
        }
    }
}