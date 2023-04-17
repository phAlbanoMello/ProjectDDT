using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f; // The movement speed
    public float runningSpeed = 8f;
    public float turnSpeed = 100f; // The turn speed
    private float speed;
    private Rigidbody rb;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runningSpeed;
            animator.Play("WalkForward", 0, 2);
        }
        else
        {
            speed = walkSpeed;
        }
    }


    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Get the horizontal input axis
        float verticalInput = Input.GetAxis("Vertical"); // Get the vertical input axis

        // Turn the object based on the horizontal input
        transform.Rotate(Vector3.up, horizontalInput * turnSpeed * Time.fixedDeltaTime);

        // Move the object towards its forward direction if the W key is pressed
        if (verticalInput > 0)
        {
            animator.Play("WalkForward");
            rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        }

        // Move the object backwards if the S key is pressed
        else if (verticalInput < 0)
        {
            animator.Play("WalkForward");
            rb.MovePosition(transform.position - transform.forward * speed * Time.fixedDeltaTime);
        }

        // Move the object left if the A key is pressed
        if (Input.GetKey(KeyCode.A))
        {
            animator.Play("WalkForward");
            rb.MovePosition(transform.position - transform.right * speed * Time.fixedDeltaTime);
        }

        // Move the object right if the D key is pressed
        else if (Input.GetKey(KeyCode.D))
        {
            animator.Play("WalkForward");
            rb.MovePosition(transform.position + transform.right * speed * Time.fixedDeltaTime);
        }

        if (verticalInput == 0)
        {
            animator.Play("Idle01");
        }
    }
}
