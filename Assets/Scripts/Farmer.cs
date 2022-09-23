using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : Controllable
{
    CharacterController characterController;

    protected override void Start()
    {
        base.Start();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateVelocity();
        Rotate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected void UpdateVelocity()
    {
        if (targetVelocity.magnitude > 0)
            velocity = Vector3.Lerp(velocity, targetVelocity, acceleration * Time.deltaTime);
        else
            velocity = Vector3.Lerp(velocity, Vector3.zero, decceleration * Time.deltaTime);

        if (!characterController.isGrounded)
            velocity.y -= gravity * Time.fixedDeltaTime;

        animator.SetFloat("Speed", new Vector2(velocity.x, velocity.z).magnitude);
    }

    private void Rotate()
    {
        if (Mathf.Abs(velocity.x) > 0f || Mathf.Abs(velocity.z) > 0f)
            transform.rotation = Quaternion.LookRotation(new Vector3(velocity.x, 0f, velocity.z));
    }

    public void Move()
    {
        characterController.Move(velocity * maxSpeed * Time.deltaTime);
    }
}
