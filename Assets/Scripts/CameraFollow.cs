using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 100f;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // GET MOUSE INPUT
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // UP DOWN CAMERA
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30f, 60f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // LEFT RIGHT PLAYER ROTATION
        player.Rotate(Vector3.up * mouseX);
    }
}