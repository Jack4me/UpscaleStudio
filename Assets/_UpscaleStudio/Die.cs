
using _UpscaleStudio._Scripts.Player;
using _UpscaleStudio._Scripts.Screens;
using UnityEngine;

namespace _UpscaleStudio {
    public class Die : MonoBehaviour
    {
        public void HandleDeath() {
            PauseHandler pause =  GameHandler.Instance.GetPause();
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ScreenHandle.instance.ShowScreen("GameOverScreen");
            // Включить анимацию смерти
            // Отключить управление игроком
            // Играть звук смерти
            // Перезагрузить уровень или показать экран с результатами
            Debug.Log("Player has died!");
            // Можно использовать методы, как `SceneManager.LoadScene` для перезагрузки уровня
            // или включить UI, который сигнализирует о конце игры.
        }
    }
}
