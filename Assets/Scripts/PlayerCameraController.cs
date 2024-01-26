using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float cameraSensitivity = 100f;

    [SerializeField] private float maxAngleX = 10f;
    [SerializeField] private float maxAngleY = 10f;

    private float xRot = default;
    private float yRot = default;

    private Transform mainCam = null;

    private void Awake()
    {
        mainCam = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CalculateRotation();
        Rotate();
    }

    private void CalculateRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yRot += mouseX * cameraSensitivity;
        xRot -= mouseY * cameraSensitivity;

        xRot = Mathf.Clamp(xRot, -maxAngleX, maxAngleX);
        yRot = Mathf.Clamp(yRot, -maxAngleY, maxAngleY);
    }

    private void Rotate()
    {
        mainCam.localRotation = Quaternion.Euler(xRot, yRot, 0);
    }

}