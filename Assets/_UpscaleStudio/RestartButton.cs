
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour {
    [SerializeField] private Button restartButton;

    void Start() {
        restartButton.onClick.AddListener(RestartGame);
    }


    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}