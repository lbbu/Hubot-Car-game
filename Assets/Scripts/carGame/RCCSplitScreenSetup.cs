using UnityEngine;

public class RCCSplitScreenSetup : MonoBehaviour
{
    public RCC_Camera cam1;
    public RCC_Camera cam2;
    public RCC_CarControllerV4 car1;
    public RCC_CarControllerV4 car2;

    void Start()
    {
        // force each RCC_Camera to stick to its own car
        cam1.cameraTarget.playerVehicle = car1;
        cam2.cameraTarget.playerVehicle = car2;

        // optional: disable auto player camera handling if enabled elsewhere
        cam1.isRendering = true;
        cam2.isRendering = true;
    }
}
