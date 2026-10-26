using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject endScreenRoot;   // Panel أو Canvas النهاية
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private TextMeshProUGUI currentTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;

    const string BEST_TIME_KEY = "BestRaceTime";

    void Awake()
    {
        // نخفي شاشة النهاية في البداية
        if (endScreenRoot != null)
            endScreenRoot.SetActive(false);
    }

    // تُستدعى من RaceManager عند انتهاء السباق
    public void ShowEndScreen(int winnerPlayerNumber, float finalTime)
    {
        if (endScreenRoot != null)
            endScreenRoot.SetActive(true);

        // 1) مين فاز
        if (winnerText != null)
            winnerText.text = $"Player {winnerPlayerNumber} Wins!";

        // 2) الوقت الحالي
        if (currentTimeText != null)
            currentTimeText.text = $"Time: {FormatTime(finalTime)}";

        // 3) أفضل وقت (من PlayerPrefs)
        float bestTime = -1f;
        if (PlayerPrefs.HasKey(BEST_TIME_KEY))
            bestTime = PlayerPrefs.GetFloat(BEST_TIME_KEY);

        if (bestTimeText != null)
        {
            if (bestTime > 0f)
                bestTimeText.text = $"Best: {FormatTime(bestTime)}";
            else
                bestTimeText.text = "Best: --:--.--";
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        float seconds = time % 60f;
        return string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }
}
