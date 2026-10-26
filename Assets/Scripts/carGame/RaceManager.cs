using UnityEngine;
using System;

public class RaceManager : MonoBehaviour
{
    [Header("Players")]
    public RCC_CarControllerV4 player1Car;
    public RCC_CarControllerV4 player2Car;

    [Header("Timer")]
    [SerializeField] private RaceTimer raceTimer;

    [Header("UI")]
    [SerializeField] private EndScreen endScreen;    // اسحب سكربت EndScreen هنا

    private bool raceFinished = false;
    const string BEST_TIME_KEY = "BestRaceTime";

    void Start()
    {
        PlayerCheckpoint.OnPlayerCompletedAllCheckpoints += PlayerWonRace;
    }

    void PlayerWonRace(int playerNumber)
    {
        if (raceFinished) return;
        raceFinished = true;

        Debug.Log("🏁 PLAYER " + playerNumber + " WINS!");
        OnRaceComplete(playerNumber);
    }

    void OnRaceComplete(int winner)
    {
        float finalTime = 0f;

        // 1) أوقف التايمر واحصل على الوقت
        if (raceTimer != null)
        {
            raceTimer.StopTimer();
            finalTime = raceTimer.GetFinalTime();
        }

        // 2) حدّث أفضل وقت بـ PlayerPrefs (أصغر وقت هو الأفضل) [web:82][web:97]
        if (finalTime > 0f)
        {
            if (!PlayerPrefs.HasKey(BEST_TIME_KEY))
            {
                PlayerPrefs.SetFloat(BEST_TIME_KEY, finalTime);
                PlayerPrefs.Save();
            }
            else
            {
                float currentBest = PlayerPrefs.GetFloat(BEST_TIME_KEY);
                if (finalTime < currentBest)
                {
                    PlayerPrefs.SetFloat(BEST_TIME_KEY, finalTime);
                    PlayerPrefs.Save();
                }
            }
        }

        // 3) أوقف اللعبة (الفيزياء)
        Time.timeScale = 0f;

        // 4) أظهر شاشة النهاية مع الفائز + الوقت الحالي + أفضل وقت
        if (endScreen != null)
            endScreen.ShowEndScreen(winner, finalTime);

        // لو حاب ترجع تعمل Restart بعد زر من UI بدال Invoke
        // فاشطب Invoke(RestartRace)
    }

    void RestartRace()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void OnDestroy()
    {
        PlayerCheckpoint.OnPlayerCompletedAllCheckpoints -= PlayerWonRace;
    }
}
