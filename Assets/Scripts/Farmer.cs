using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : Controllable
{
    // Rigidbody rb;
    Animator animator;
    CharacterController characterController;

    private Vector3 targetVelocity;

    private void Start()
    {
        // rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateVelocity();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void UpdateVelocity()
    {
        if (targetVelocity.magnitude > 0)
            velocity = Vector3.Lerp(velocity, targetVelocity, acceleration * Time.deltaTime);
        else
            velocity = Vector3.Lerp(velocity, Vector3.zero, decceleration * Time.deltaTime);

        if (!characterController.isGrounded)
            velocity.y -= gravity * Time.fixedDeltaTime;

        animator.SetFloat("Speed", velocity.magnitude);
        animator.SetFloat("Vertical", velocity.y);

        print("Update :" + velocity);
    }

    private void Move()
    {
        // rb.MovePosition(rb.position + velocity * Time.deltaTime);
        // rb.velocity = velocity;
        characterController.Move(velocity * maxSpeed * Time.deltaTime);

        //Rotate in Movement Direction
        if (velocity.magnitude > 0)
            transform.rotation = Quaternion.LookRotation(new Vector3(velocity.x, 0f, velocity.z));

        // print(rb.velocity);
        // Vector3 newPosition = transform.position + velocity;

        // rb.MovePosition(newPosition);
    }

    public override void OnMove(Vector2 movementVector)
    {
        this.targetVelocity = new Vector3(movementVector.x, 0, movementVector.y);
    }
}
