using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperSkeletonMovement : MonoBehaviour
{
    public bool CanMove
    {
        get
        {
            return canMove;
        }
        set
        {
            canMove = value;
            if (!canMove)
            {
                animator.SetBool("IsRunning", false);
            }
        }
    }
    private bool canMove = false;

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float maxSpeed;

    private Rigidbody rb;
    private Animator animator;
    private float distToGround;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  
        animator = GetComponentInChildren<Animator>();  
    }

    void FixedUpdate()
    {

        if (IsGrounded() && (rb.velocity.x < 0.001f && rb.velocity.x > -0.001f))
        {
            rb.velocity = Vector3.zero;
        }

        if (!canMove)
        {
            return;
        }

        if(rb.velocity.x > maxSpeed)
            rb.velocity = new Vector3(maxSpeed, rb.velocity.y, rb.velocity.z);
        else if(rb.velocity.x < -maxSpeed)
            rb.velocity = new Vector3(-maxSpeed, rb.velocity.y, rb.velocity.z);
        //rb.velocity.x = Vector3.ClampMagnitude(rb.velocity.x, maxSpeed);
        float horizontalInput = Input.GetAxis ("Horizontal");

        // Flip the skeleton depending on which direction he's going.
        if(rb.velocity.x > 0)
            transform.GetChild(0).eulerAngles = Vector3.zero;
        else if(rb.velocity.x < 0)
            transform.GetChild(0).eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);

        Vector3 movement = new Vector3 (horizontalInput, 0.0f, 0.0f);
        rb.AddForce(movement * speed, ForceMode.Impulse);

        if(rb.velocity.x == 0)
        {
            animator.SetBool("IsRunning", false);
        }
        else if(rb.velocity.x != 0 && IsGrounded())
        {
            Debug.Log("ADSSADASD");
            animator.SetBool("IsRunning", true);
        }

        // Jump if on the ground.
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            animator.SetTrigger("Jumped");
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }

        if(rb.velocity.y < 1)
        {
            rb.velocity += Vector3.down * 20 * Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        if(isGrounded)
            animator.SetBool("Grounded", true);
        else
            animator.SetBool("Grounded", false);

        return isGrounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            isGrounded = true;
            animator.SetBool("Grounded", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            isGrounded = false;
            animator.SetBool("Grounded", false);
        }
    }
}
