using UnityEngine;
using System.Collections;
using TMPro;

public class RaceCountdown : MonoBehaviour
{
    [Header("UI Setup")]
    [SerializeField] private TextMeshProUGUI countdownText;

    [Header("Timing")]
    [SerializeField] private float timePerNumber = 1f;

    [Header("RCC Cars")]
    [SerializeField] private RCC_CarControllerV4[] playerCars;

    private bool raceStarted = false;

    void Start()
    {
        DisableCarControlsOnly();
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(timePerNumber);

        countdownText.text = "2";
        yield return new WaitForSeconds(timePerNumber);

        countdownText.text = "1";
        yield return new WaitForSeconds(timePerNumber);

        countdownText.text = "GO!";
        yield return new WaitForSeconds(timePerNumber);

        OnCountdownFinish();
    }

    // تعطيل التحكم فقط - الكونترولر يبقى شغال للـ NOS
    private void DisableCarControlsOnly()
    {
        foreach (var car in playerCars)
        {
            if (car != null)
            {
                car.canControl = false;           // منع الإدخال
                // car.enabled = false;          // شيل هذا السطر!
            }
        }
    }

    private void OnCountdownFinish()
    {
        Debug.Log("Race Started! 🚀");

        // فعّل التحكم فقط
        foreach (var car in playerCars)
        {
            if (car != null)
            {
                car.canControl = true;            // اللاعب يقدر يتحكم
                // car.enabled = true;             // مش محتاج لأنه ما اتعطل أصلاً
            }
        }

        raceStarted = true;
        countdownText.gameObject.SetActive(false);
    }
}
