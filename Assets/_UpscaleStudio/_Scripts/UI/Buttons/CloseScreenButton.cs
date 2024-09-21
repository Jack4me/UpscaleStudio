using _UpscaleStudio._Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _UpscaleStudio._Scripts.Screens.Buttons {
   public class CloseButton : MonoBehaviour {
      public Button CloseHUDButton;


      private void Start() {
         CloseHUDButton.onClick.AddListener(ScreenHandle.instance.GoBack);
      }

      
   }
}
