using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float rotationSpeed = 120f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private bool isGrounded;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Better physics settings
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        // ROTATE PLAYER
        transform.Rotate(Vector3.up * horizontal * rotationSpeed * Time.deltaTime);

        // CHECK RUN
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // CURRENT SPEED
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // MOVEMENT
        Vector3 move = transform.forward * vertical * currentSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + move);

        // ANIMATIONS
        bool isWalking = Mathf.Abs(vertical) > 0.1f;

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning && isWalking);

        // JUMP
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // STOP DRIFTING
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        rb.angularVelocity = Vector3.zero;
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}