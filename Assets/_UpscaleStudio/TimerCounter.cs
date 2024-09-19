
using TMPro;
using UnityEngine;

public class TimerCounter : MonoBehaviour {

    [SerializeField ] private TextMeshProUGUI  timerText;  
    private float timer;    
    private bool isPaused;  

    private void Start()
    {
        timer = 0f;
        isPaused = false;
    }

    private void Update()
    {
        if (!isPaused)
        {
            timer += Time.deltaTime;  
            UpdateTimerUI();
        }
    }

    
    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    
    public void PauseTimer()
    {
        isPaused = true;
    }

  
    public void ResumeTimer()
    {
        isPaused = false;
    }
}

