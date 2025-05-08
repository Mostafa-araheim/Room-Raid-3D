using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float gravity = -9.81f;
    public float walkSpeed = 12f;
    public float sprintSpeed = 18f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public AudioSource footstepSource;
    public AudioClip footstepClip;
    public float walkFootstepInterval = 0.5f;
    public float sprintFootstepInterval = 0.3f;

    private bool isGrounded;
    private Vector3 velocity;
    private float footstepTimer;
    private float currentSpeed;
    private float currentFootstepInterval;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
        currentFootstepInterval = isSprinting ? sprintFootstepInterval : walkFootstepInterval;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);


        if (isGrounded && move.magnitude > 0.1f)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                PlayFootstep();
                footstepTimer = currentFootstepInterval;
            }
        }
        else
        {
            footstepTimer = 0f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(2f * -gravity * jumpHeight);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void PlayFootstep()
    {
        if (footstepClip != null && footstepSource != null)
        {
            footstepSource.PlayOneShot(footstepClip);
        }
    }
}
