using UnityEngine;

public class RaceCheckpointOne : MonoBehaviour
{
    [Header("Player Number")]
    public int playerNumber = 1; // 1 أو 2

    private Renderer rend;
    private bool isHit = false;

    void Start()
    {
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;

        rend = GetComponent<Renderer>();
        Debug.Log("🚩 CP for Player " + playerNumber + " ready");
    }

    void OnTriggerEnter(Collider other)
    {
        // تحقق من السيارة المناسبة
        RCC_CarControllerV4 car = other.GetComponentInParent<RCC_CarControllerV4>();
        if (car != null && !isHit)
        {
            // Player 1 car name يحتوي "Player1" أو رقم 1
            if ((playerNumber == 1 && other.transform.root.name.Contains("Player1"))
                )
            {
                isHit = true;
                rend.enabled = false;
                Debug.Log("✅ Player " + playerNumber + " hit CP!");
            }
        }
    }
}
