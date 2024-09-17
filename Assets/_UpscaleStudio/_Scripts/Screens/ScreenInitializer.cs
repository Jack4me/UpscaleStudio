using UnityEngine;
using UpscaleStudio._Scripts.Player.Screen;

namespace _UpscaleStudio._Scripts.Screens {
    public class ScreenInitializer : MonoBehaviour
    {
       
            [SerializeField] private string screenName;

            void Awake() {
                // Screen register 
                ScreenManager.instance.RegisterScreen(screenName, gameObject);
                if (gameObject.CompareTag("Menu") ) {
                    return;
                }
                gameObject.SetActive(false);
            }
        
    }
}
