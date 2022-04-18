using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public GameplaySettings gameplaySettings;
    public Transform player;
    public float mouseSensitivity;
    float xRotation;
    float mouseX;
    [SerializeField] float hidingMaxRotation;
    [SerializeField] float hidingMinRotation;
    float minXRotation;
    float maxXRotation;
    [HideInInspector] public bool isHiding;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isHiding = false;

        mouseSensitivity = gameplaySettings.GetSensitivity();
    }

    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        Hide(isHiding);
        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void Hide(bool startHiding)
    {
        if (startHiding)
        {
            minXRotation = hidingMinRotation;
            maxXRotation = hidingMaxRotation;
            player.Rotate(Vector3.zero);
        }
        else if (!startHiding)
        {
            minXRotation = -90f;
            maxXRotation = 90f;
            player.Rotate(Vector3.up * mouseX);
        }
    }

    public void ChangeSensitivity(float value)
    {
        mouseSensitivity = value;
    }
}
