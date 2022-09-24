using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    protected PlayerController playerController;
    protected CharacterController characterController;
    protected Animator animator;

    [SerializeField] protected float maxSpeed = 10f;
    [SerializeField] protected float acceleration = 5f;
    [SerializeField] protected float decceleration = 5f;
    [SerializeField] protected float gravity = 9.81f;
    protected Vector3 velocity;
    protected Vector3 targetVelocity;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    protected void Update()
    {
        UpdateVelocity();
        Rotate();
    }

    protected void FixedUpdate()
    {
        Move();
    }

    public virtual void InitPlayerController(PlayerController _playerController)
    {
        playerController = _playerController;
    }

    public virtual void OnMove(Vector2 movementVector)
    {
        this.targetVelocity = new Vector3(movementVector.x, 0, movementVector.y);
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

    public void ResetVelocity()
    {
        velocity = Vector3.zero;
        targetVelocity = Vector3.zero;
    }

    protected void Rotate()
    {
        if (Mathf.Abs(velocity.x) > 0.1f || Mathf.Abs(velocity.z) > 0.1f)
            transform.rotation = Quaternion.LookRotation(new Vector3(velocity.x, 0f, velocity.z));
    }

    protected void Move()
    {
        characterController.Move(velocity * maxSpeed * Time.deltaTime);
    }

    public virtual void OnBaseInteract()
    {
        Debug.Log("Base interact");
    }

    public virtual void OnSpecialInteract()
    {
        Debug.Log("Special interact");
    }
}
