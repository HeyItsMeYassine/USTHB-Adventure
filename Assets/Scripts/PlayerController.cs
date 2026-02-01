using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.7f;
    public float rotationSpeed = 10f;

    [Header("Jump & Gravity")]
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    // 🔒 Movement lock (used by dialogue / quiz)
    public bool canMove = true;

    private CharacterController controller;
    private Animator animator;

    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 🔴 If movement is locked (dialogue / quiz)
        if (!canMove)
        {
            animator.SetFloat("Speed", 0f);
            return;
        }

        HandleMovement();
        HandleGravityAndJump();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Camera-relative movement
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        Vector3 move = camForward.normalized * vertical + camRight.normalized * horizontal;
        move = Vector3.ClampMagnitude(move, 1f);

        float speed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
            speed *= sprintMultiplier;

        // Rotate player toward movement direction
        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Move character
        controller.Move(move * speed * Time.deltaTime);

        // Drive animation using input magnitude
        animator.SetFloat("Speed", move.magnitude);
    }

    void HandleGravityAndJump()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
