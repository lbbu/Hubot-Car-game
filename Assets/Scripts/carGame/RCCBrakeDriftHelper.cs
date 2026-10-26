using UnityEngine;

public class RCCBrakeDriftHelper : MonoBehaviour
{
    public RCC_CarControllerV4 car;

    [Header("Drift Settings")]
    [Range(0f, 1f)]
    public float rearGripWhenBraking = 0.5f;   // 0.5 = half grip while braking

    private float defaultRearStiffness;
    private WheelCollider rearLeftWC;
    private WheelCollider rearRightWC;

    void Start()
    {
        if (car == null)
            car = GetComponent<RCC_CarControllerV4>();

        if (car == null)
            return;

        // Get the underlying WheelCollider components from RCC_WheelCollider objects
        if (car.RearLeftWheelCollider != null)
            rearLeftWC = car.RearLeftWheelCollider.GetComponent<WheelCollider>();

        if (car.RearRightWheelCollider != null)
            rearRightWC = car.RearRightWheelCollider.GetComponent<WheelCollider>();

        // Store default stiffness from one rear wheel (fallback to 1 if missing)
        if (rearLeftWC != null)
            defaultRearStiffness = rearLeftWC.sidewaysFriction.stiffness;
        else
            defaultRearStiffness = 1f;
    }

    void FixedUpdate()
    {
        if (car == null || (rearLeftWC == null && rearRightWC == null))
            return;

        float brakeInput = car.inputs.brakeInput;   // 0–1

        // Lower stiffness when braking, return to default when not
        float targetStiffness = Mathf.Lerp(
            defaultRearStiffness,
            defaultRearStiffness * rearGripWhenBraking,
            brakeInput
        );

        ApplyRearSideFriction(targetStiffness);
    }

    void ApplyRearSideFriction(float stiffness)
    {
        if (rearLeftWC != null)
        {
            var f = rearLeftWC.sidewaysFriction;
            f.stiffness = stiffness;
            rearLeftWC.sidewaysFriction = f;
        }

        if (rearRightWC != null)
        {
            var f = rearRightWC.sidewaysFriction;
            f.stiffness = stiffness;
            rearRightWC.sidewaysFriction = f;
        }
    }
}
