using System;
using System.Collections;
using _UpscaleStudio._Scripts.Items;
using _UpscaleStudio._Scripts.Player;
using _UpscaleStudio._Scripts.Screens;
using _UpscaleStudio._Scripts.System.Handlers;
using UnityEngine;

namespace _UpscaleStudio._Scripts.System {
    public class WinGame : MonoBehaviour {
        [SerializeField] private LootHandle lootHandle;
        [SerializeField] private PlayerHandle player;
        private int keysCount;

        private IEnumerator InitializeLootHandleWithDelay() {
            yield return new WaitForEndOfFrame();  // Ждём завершения текущего кадра
            lootHandle = LootHandle.Instance;
    
            if (lootHandle != null) {
                lootHandle.ChangedAction += UpdateKeysCount;
            } else {
                Debug.LogError("LootHandle instance is missing!");
            }
        }

        private void Start() {
            StartCoroutine(InitializeLootHandleWithDelay());  // Запускаем инициализацию с задержкой
        }
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
                    player.playerCamera.DisableRotation();
                }
            }

        
        }
    }
}
