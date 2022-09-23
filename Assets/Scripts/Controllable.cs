using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    [SerializeField] protected float maxSpeed = 10f;
    [SerializeField] protected float acceleration = 5f;
    [SerializeField] protected float decceleration = 5f;
    [SerializeField] protected float gravity = 9.81f;
    protected Vector3 velocity;
    public virtual void OnMove(Vector2 movementVector) { }
}
