using UnityEngine;

public class RCCBoostState : MonoBehaviour
{
    public bool isBoosting = false;
    public float boostTimer = 0f;
    public float boostDuration = 2f;

    public void StartBoost(float duration)
    {
        boostDuration = duration;
        boostTimer = duration;
        isBoosting = true;
    }

    void Update()
    {
        if (!isBoosting)
            return;

        boostTimer -= Time.deltaTime;
        if (boostTimer <= 0f)
        {
            isBoosting = false;
            boostTimer = 0f;
        }
    }
}
