using UnityEngine;
using UpscaleStudio._Scripts.Player.Screen.Scene;

namespace UpscaleStudio._Scripts.Player.Screen {
    public class LoaderCallBack : MonoBehaviour {
        private bool IsFirstUpdate = true;

        private void Update() {
            if (IsFirstUpdate) {
                IsFirstUpdate = false;
                Invoke("InvokeLoaderCallback", 1f);
            }
        }

        private void InvokeLoaderCallback() {
            SceneHandler.LoaderCallBack();
        }
    }
}