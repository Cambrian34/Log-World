using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8.0f;
    [SerializeField] private float acceleration = 60.0f;
    [SerializeField] private float deceleration = 80.0f;
    [SerializeField] private float jumpForce = 8.0f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.3f;

    public Rigidbody rb;

    private bool isGrounded;
    private float horizontalInput;
    private float verticalInput;

    void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void FixedUpdate()
    {
        HandleHorizontalMovement();
    }

    private void HandleHorizontalMovement()
    {
        
        Vector3 moveDirection = (transform.forward * verticalInput + transform.right * horizontalInput).normalized;

        Vector3 targetVelocity = moveDirection * moveSpeed;
        Vector3 currentFlatVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        Vector3 velocityChange = targetVelocity - currentFlatVelocity;

        Vector3 force = Vector3.zero;
        if (moveDirection.magnitude > 0.1f)
        {
            force = velocityChange * acceleration;
        }
        else
        {
            force = -currentFlatVelocity * deceleration;
        }

        rb.AddForce(force * rb.mass, ForceMode.Acceleration);

        Vector3 limitedFlatVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        if (limitedFlatVelocity.magnitude > moveSpeed)
        {
            limitedFlatVelocity = limitedFlatVelocity.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedFlatVelocity.x, rb.linearVelocity.y, limitedFlatVelocity.z);
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
