using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour {
    [SerializeField] private Button ExitGameButton;

    private void Start() {
        ExitGameButton.onClick.AddListener(OnExitGameButtonClick);
    }

    private void OnExitGameButtonClick() {
        Debug.Log("Exiting Game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}