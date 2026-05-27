using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float rotationSmoothTime = 0.1f;

    CharacterController controller;
    Animator animator;

    float rotationSmoothVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        float speed = 0f;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle =
                Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg
                + Camera.main.transform.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref rotationSmoothVelocity,
                rotationSmoothTime
            );

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir =
                Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            speed = Input.GetKey(KeyCode.LeftShift)
                ? runSpeed
                : walkSpeed;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // ANIMATION
        animator.SetFloat("Speed", speed);
    }
}