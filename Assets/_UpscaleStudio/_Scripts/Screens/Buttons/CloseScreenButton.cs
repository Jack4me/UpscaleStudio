using UnityEngine;
using UnityEngine.UI;

namespace UpscaleStudio._Scripts.Player.Screen {
   public class CloseButton : MonoBehaviour {
      public GameObject window; 
      public Button CloseHUDButton;


      private void Start() {
         CloseHUDButton.onClick.AddListener(ScreenManager.instance.GoBack);
      }

      public void CloseWindow() {
         window.SetActive(false);
      }
   }
}
