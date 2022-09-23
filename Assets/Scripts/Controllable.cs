using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    protected PlayerController playerController;
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
    }

    public virtual void InitPlayerController(PlayerController _playerController)
    {
        playerController = _playerController;
    }

    public virtual void OnMove(Vector2 movementVector)
    {
        this.targetVelocity = new Vector3(movementVector.x, 0, movementVector.y);
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
