using UnityEngine;
using TMPro;

public class RaceTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float elapsedTime = 0f;
    private bool isRunning = false;

    void Start()
    {
        elapsedTime = 0f;
        isRunning = true;
        UpdateTimerText();
    }

    void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        float seconds = elapsedTime % 60f;
        timerText.text = string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public float GetFinalTime()
    {
        return elapsedTime;
    }
}
