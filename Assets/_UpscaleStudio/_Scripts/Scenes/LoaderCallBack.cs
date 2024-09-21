using _UpscaleStudio._Scripts.System.Handlers;
using UnityEngine;

namespace _UpscaleStudio._Scripts.Scenes {
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