using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraBehavior : MonoBehaviour
{

    [Header("Settings")]
    public float sensitivity = 1;
    public float smoothTime = 0.1f;

    [Header("References")]
    public Transform playerTransform;
    public Camera playerCamera;

    float xRotation = 0;
    Vector2 currentRotation;
    Vector2 currentRotationVelocity;

    public void UpdateCamera(Vector2 cameraMovement)
    {
        float targetRotationY = cameraMovement.y * sensitivity * Time.deltaTime;
        float targetRotationX = cameraMovement.x * sensitivity * Time.deltaTime;

        xRotation -= targetRotationY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        currentRotation.x = Mathf.SmoothDamp(currentRotation.x, xRotation, ref currentRotationVelocity.x, smoothTime);

        currentRotation.y = Mathf.SmoothDamp(currentRotation.y, targetRotationX, ref currentRotationVelocity.y, smoothTime);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerTransform.Rotate(Vector3.up, currentRotation.y);
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
