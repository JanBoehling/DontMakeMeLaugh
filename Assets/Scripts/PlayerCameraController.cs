using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1f;
    [SerializeField, Range(0f, 1f)] private float springBackSpeed = .6f;

    [SerializeField] private Vector2 dampAngle = new(40f, 20f);
    [SerializeField] private Vector2 maxAngle = new(60f, 60f);

    [SerializeField] private Transform mainCam;

    private float xRot = default;
    private float yRot = default;

    private float smoothDampVelocityX = default;
    private float smoothDampVelocityY = default;

    private void Awake()
    {
        EnableCamera();
    }

    private void Update()
    {
        CalculateRotation();
        Rotate();
    }

    private void EnableCamera()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void CalculateRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yRot += mouseX * sensitivity;
        xRot -= mouseY * sensitivity;

        if (xRot > dampAngle.x || xRot < -dampAngle.x) xRot = Mathf.SmoothDamp(xRot, Mathf.Sign(xRot) * dampAngle.x, ref smoothDampVelocityX, 1 - springBackSpeed);
        if (yRot > dampAngle.y || yRot < -dampAngle.y) yRot = Mathf.SmoothDamp(yRot, Mathf.Sign(yRot) * dampAngle.y, ref smoothDampVelocityY, 1 - springBackSpeed);

        xRot = Mathf.Clamp(xRot, -maxAngle.x, maxAngle.x);
        yRot = Mathf.Clamp(yRot, -maxAngle.y, maxAngle.y);
    }

    private void Rotate()
    {
        mainCam.localRotation = Quaternion.Euler(xRot, yRot, 0f);
    }
}