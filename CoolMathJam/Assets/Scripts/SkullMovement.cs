using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SkullMovement : MonoBehaviour
{
    public float RollPower = 5000f;
    public float JumpPower = 10f;
    public float FallAccelerationFactor = 2f;

    private Rigidbody rb;
    private bool jumped = false;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * RollPower, ForceMode.Acceleration);

        if (!jumped && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            jumped = true;
        }
        else if (jumped)
        {
            float holdJumpFactor = 1f;
            if (Input.GetButton("Jump"))
                holdJumpFactor = 0.5f;
            rb.AddForce(Vector3.down * FallAccelerationFactor * Time.deltaTime * holdJumpFactor, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            jumped = false;
        }
    }
}
