using UnityEngine;

public class RaceCheckpointsTwo : MonoBehaviour
{
    [Header("Player Number")]
    public int playerNumber = 2; // **غيّر هون من 1 إلى 2**

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
        RCC_CarControllerV4 car = other.GetComponentInParent<RCC_CarControllerV4>();
        if (car != null && !isHit)
        {
            Debug.Log("🎯 HIT by: " + other.transform.root.name + " | Checking Player " + playerNumber);

            // تحقق من السيارة المناسبة
            if (other.transform.root.name.Contains("Player" + playerNumber))
            {
                isHit = true;
                rend.enabled = false;
                Debug.Log("✅ Player " + playerNumber + " hit CP!");
            }
        }
    }
}
