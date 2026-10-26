using UnityEngine;
using System;

public class PlayerCheckpoint : MonoBehaviour
{
    [Header("Player Number")]
    public int playerNumber = 1; // 1 أو 2

    [Header("Checkpoint Number")]
    public int checkpointNumber = 1; // 1, 2, 3...

    private Renderer rend;
    private bool isHit = false;
    public static event Action<int> OnPlayerCompletedAllCheckpoints; // حدث عند إكمال الكل

    void Start()
    {
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
        rend = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        RCC_CarControllerV4 car = other.GetComponentInParent<RCC_CarControllerV4>();
        if (car != null && !isHit)
        {
            if (other.transform.root.name.Contains("Player" + playerNumber))
            {
                isHit = true;
                rend.enabled = false;
                Debug.Log("✅ Player " + playerNumber + " hit CP " + checkpointNumber);

                // تحقق إذا كمل كل الـ Checkpoints
                CheckIfRaceCompleted(playerNumber);
            }
        }
    }

    void CheckIfRaceCompleted(int playerNum)
    {
        // عد كم Checkpoint مكمل اللاعب
        int completedCPs = 0;
        PlayerCheckpoint[] allCPs = FindObjectsOfType<PlayerCheckpoint>();

        foreach (var cp in allCPs)
        {
            if (cp.playerNumber == playerNum && cp.isHit)
                completedCPs++;
        }

        // لو كمل كل الـ Checkpoints (افترض 5 مثلاً)
        if (completedCPs >= 11)
        {
            Debug.Log("🏆 PLAYER " + playerNum + " COMPLETED ALL CHECKPOINTS!");
            OnPlayerCompletedAllCheckpoints?.Invoke(playerNum);
        }
    }
}
