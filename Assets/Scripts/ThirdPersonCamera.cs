using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;

    public Vector3 offset = new Vector3(0f, 2f, -5f);

    public float mouseSensitivity = 200f;
    public float smoothSpeed = 10f;

    float yaw;
    float pitch = 15f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, -20f, 60f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        Vector3 desiredPosition = player.position + rotation * offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}