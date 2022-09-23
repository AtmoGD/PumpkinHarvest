using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : Controllable
{
    CharacterController characterController;

    [SerializeField] private Transform interactPoint;
    [SerializeField] private float interactRadius = 1f;

    private IInteractable interactableInReach;

    protected override void Start()
    {
        base.Start();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateInteractableInReach();
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
            velocity.y -= gravity * Time.deltaTime;

        animator.SetFloat("Speed", new Vector2(velocity.x, velocity.z).magnitude);
    }

    private void Rotate()
    {
        if (Mathf.Abs(velocity.x) > 0.1f || Mathf.Abs(velocity.z) > 0.1f)
            transform.rotation = Quaternion.LookRotation(new Vector3(velocity.x, 0f, velocity.z));
    }

    public void Move()
    {
        characterController.Move(velocity * maxSpeed * Time.deltaTime);
    }

    public override void OnBaseInteract()
    {
        interactableInReach?.BaseInteract();
    }

    public void UpdateInteractableInReach()
    {
        RaycastHit[] hits = Physics.SphereCastAll(interactPoint.position, interactRadius, Vector3.up, 0f);

        foreach (RaycastHit hit in hits)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactableInReach = interactable;
                return;
            }
        }

        interactableInReach = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactPoint.position, interactRadius);
    }
}
