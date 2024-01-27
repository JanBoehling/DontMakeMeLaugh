using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1f;
    [SerializeField, Range(0f, 1f)] private float springBackSpeed = .6f;

    [SerializeField] private Vector2 maxAngle = new(60f, 60f);

    private float xRot = default;
    private float yRot = default;

    private Transform mainCam = null;

    private float smoothDampVelocityX = default;
    private float smoothDampVelocityY = default;

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

        yRot += mouseX * sensitivity;
        xRot -= mouseY * sensitivity;

        xRot = Mathf.SmoothDamp(xRot, 0f, ref smoothDampVelocityX, 1 - springBackSpeed);
        yRot = Mathf.SmoothDamp(yRot, 0f, ref smoothDampVelocityY, 1 - springBackSpeed);

        xRot = Mathf.Clamp(xRot, -maxAngle.x, maxAngle.x);
        yRot = Mathf.Clamp(yRot, -maxAngle.y, maxAngle.y);
    }

    private void Rotate()
    {
        mainCam.localRotation = Quaternion.Euler(xRot, yRot, 0f);
    }
}