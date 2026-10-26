using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BoostPickup : MonoBehaviour
{
    [Header("Boost Settings")]
    public float boostDuration = 2f;
    public bool hideOnPickup = true;

    private Collider col;
    private Renderer[] renderers;

    void Awake()
    {
        col = GetComponent<Collider>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        // Find car
        RCC_CarControllerV4 car = other.GetComponentInParent<RCC_CarControllerV4>();
        if (car == null)
            return;

        // Get/ensure boost state
        RCCBoostState boostState = car.GetComponent<RCCBoostState>();
        if (boostState == null)
            boostState = car.gameObject.AddComponent<RCCBoostState>();

        // Start boost
        boostState.StartBoost(boostDuration);

        // Visually hide pickup
        if (hideOnPickup)
        {
            if (col != null) col.enabled = false;
            if (renderers != null)
            {
                foreach (var r in renderers)
                    r.enabled = false;
            }
        }
    }
}
