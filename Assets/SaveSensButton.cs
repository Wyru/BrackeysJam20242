using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSensButton : MonoBehaviour
{
    public void Save()
    {
        CameraController.instance.enabled = true;
        InputManager.instance.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void AdjustSensitivity(float newSpeed)
    {
        CameraController.instance._sensitivity = newSpeed;

    }
}
