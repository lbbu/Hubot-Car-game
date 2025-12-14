using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class BoostPickup : MonoBehaviour
{
    [Header("Boost Settings")]
    public float boostDuration = 2f;
    public float boostStrength = 1f;

    [Tooltip("هل يختفي الأيتم بصرياً بعد الالتقاط؟")]
    public bool hideOnPickup = true;

    private Collider col;
    private Renderer[] renderers;

    private void Awake()
    {
        col = GetComponent<Collider>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        RCC_CarControllerV4 car = other.GetComponentInParent<RCC_CarControllerV4>();
        if (car == null)
            return;

        // إخفاء الأيتم بدون تدمير
        if (hideOnPickup)
        {
            if (col != null)
                col.enabled = false;

            if (renderers != null)
            {
                foreach (var r in renderers)
                    r.enabled = false;
            }
        }

        StartCoroutine(DoBoost(car));
    }

    private IEnumerator DoBoost(RCC_CarControllerV4 car)
    {
        float timer = 0f;

        while (timer < boostDuration)
        {
            RCC_Inputs currentInputs = car.inputs;

            // ثروتل للأمام أثناء البوست
            if (currentInputs.throttleInput < 0.5f)
                currentInputs.throttleInput = 1f;

            currentInputs.boostInput = boostStrength;

            car.OverrideInputs(currentInputs);

            timer += Time.deltaTime;
            yield return null;
        }

        RCC_Inputs resetInputs = car.inputs;
        resetInputs.boostInput = 0f;
        car.OverrideInputs(resetInputs);

        // إذا حاب تدمّر الأيتم بعد انتهاء البوست فعلاً:
        // Destroy(gameObject);
    }
}
