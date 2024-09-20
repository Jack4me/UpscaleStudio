
using _UpscaleStudio._Scripts;
using _UpscaleStudio._Scripts.Items;
using _UpscaleStudio._Scripts.Player;
using _UpscaleStudio._Scripts.Screens;
using UnityEngine;

public class WinGame : MonoBehaviour {
    [SerializeField] private LootHandle lootHandle;
    private int keysCount;

    private void OnEnable() {
        lootHandle.ChangedAction += UpdateKeysCount;
    }

    private void OnDisable() {
        lootHandle.ChangedAction -= UpdateKeysCount;
    }

    private void UpdateKeysCount() {
        keysCount = lootHandle.Collected;
    }

    private void OnTriggerEnter(Collider other) {
        if (keysCount >= 3) {
            if (other.TryGetComponent(out PlayerHandle playerHandle)) {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                ScreenHandle.instance.ShowScreen("WinScreen");
                Debug.Log("YOU WIN");
                SoundHandler.Instance.StopAllSounds();
            }
        }

        
    }
}
