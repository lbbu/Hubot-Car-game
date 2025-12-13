using UnityEngine;

public class TwoPlayerCarController : MonoBehaviour
{
    [Header("Player 1 - WASD")]
    public RCC_CarControllerV4 player1Car;

    [Header("Player 2 - Arrow Keys")]
    public RCC_CarControllerV4 player2Car;

    void Start()
    {
        // تفعيل Override للسيارتين
        if (player1Car != null)
            player1Car.overrideInputs = true;

        if (player2Car != null)
            player2Car.overrideInputs = true;
    }

    void Update()
    {
        // Player 1: WASD
        if (player1Car != null)
        {
            RCC_Inputs player1Inputs = new RCC_Inputs();

            // Steering
            float p1Horizontal = 0f;
            if (Input.GetKey(KeyCode.A)) p1Horizontal = -1f;
            if (Input.GetKey(KeyCode.D)) p1Horizontal = 1f;

            // Throttle/Brake
            float p1Vertical = 0f;
            if (Input.GetKey(KeyCode.W)) p1Vertical = 1f;
            if (Input.GetKey(KeyCode.S)) p1Vertical = -1f;

            player1Inputs.steerInput = p1Horizontal;
            player1Inputs.throttleInput = Mathf.Clamp01(p1Vertical);
            player1Inputs.brakeInput = Mathf.Clamp01(-p1Vertical);
            player1Inputs.handbrakeInput = Input.GetKey(KeyCode.Space) ? 1f : 0f;

            player1Car.OverrideInputs(player1Inputs);
        }

        // Player 2: Arrow Keys
        if (player2Car != null)
        {
            RCC_Inputs player2Inputs = new RCC_Inputs();

            // Steering
            float p2Horizontal = 0f;
            if (Input.GetKey(KeyCode.LeftArrow)) p2Horizontal = -1f;
            if (Input.GetKey(KeyCode.RightArrow)) p2Horizontal = 1f;

            // Throttle/Brake
            float p2Vertical = 0f;
            if (Input.GetKey(KeyCode.UpArrow)) p2Vertical = 1f;
            if (Input.GetKey(KeyCode.DownArrow)) p2Vertical = -1f;

            player2Inputs.steerInput = p2Horizontal;
            player2Inputs.throttleInput = Mathf.Clamp01(p2Vertical);
            player2Inputs.brakeInput = Mathf.Clamp01(-p2Vertical);
            player2Inputs.handbrakeInput = Input.GetKey(KeyCode.RightShift) ? 1f : 0f;

            player2Car.OverrideInputs(player2Inputs);
        }
    }
}
