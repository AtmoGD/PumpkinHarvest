using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : Controllable
{
    // Rigidbody rb;
    CharacterController characterController;

    private Vector3 targetVelocity;

    private void Start()
    {
        // rb = GetComponent<Rigidbody>();
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

        // velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        print("Update :" + velocity);

        velocity.y -= gravity * Time.fixedDeltaTime;
    }

    private void Move()
    {
        // rb.MovePosition(rb.position + velocity * Time.deltaTime);
        // rb.velocity = velocity;
        characterController.Move(velocity * maxSpeed * Time.deltaTime);

        // print(rb.velocity);
        // Vector3 newPosition = transform.position + velocity;

        // rb.MovePosition(newPosition);
    }

    public override void OnMove(Vector2 movementVector)
    {
        this.targetVelocity = new Vector3(movementVector.x, 0, movementVector.y);
    }
}
