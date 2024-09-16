using UnityEngine;

namespace UpscaleStudio._Scripts.Player.Screen {
    public class ScreenInitializer : MonoBehaviour
    {
       
            [SerializeField] private string screenName;

            void Awake() {
                // Регистрация экрана в реестре ScreenManager
                ScreenManager.instance.RegisterScreen(screenName, gameObject);
                if (gameObject.CompareTag("Menu") ) {
                    return;
                }
                gameObject.SetActive(false);
            }
        
    }
}
