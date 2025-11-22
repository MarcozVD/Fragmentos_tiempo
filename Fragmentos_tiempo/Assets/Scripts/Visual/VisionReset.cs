using UnityEngine;

public class VisionReset : MonoBehaviour
{
    public VisionDamageController controller;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            controller.ResetVision();
        }
    }
}
