using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickUpType
{
    None,
    Water,
    Seed,
    Pumpkin
}

public enum FarmerState
{
    Idle,
    Walking,
    Seeding,
    Watering,
    Harvesting
}

public class Farmer : Controllable
{
    CharacterController characterController;

    [SerializeField] private Transform interactPoint;
    [SerializeField] private float interactRadius = 1f;
    [SerializeField] private GameObject pumpkin;
    [SerializeField] private GameObject pumpkinPrefab;
    private PickUpType currentItem = PickUpType.None;
    public PickUpType CurrentItem { get { return currentItem; } }
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
        if (currentItem == PickUpType.None || interactableInReach != null)
        {
            interactableInReach?.BaseInteract(this);
        }
        else
        {
            DropItem();
        }
    }

    public void UpdateInteractableInReach()
    {
        RaycastHit[] hits = Physics.SphereCastAll(interactPoint.position, interactRadius, Vector3.up, 0f);

        foreach (RaycastHit hit in hits)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactableInReach?.ShowBaseInteractTooltip(false);
                interactableInReach = interactable;
                interactableInReach.ShowBaseInteractTooltip(true);
                return;
            }
        }

        interactableInReach?.ShowBaseInteractTooltip(false);
        interactableInReach = null;
    }

    public bool PickUpItem(PickUpType _item)
    {
        if (CurrentItem == PickUpType.None)
        {
            currentItem = _item;
            animator.SetBool("HasItem", true);

            switch (currentItem)
            {
                case PickUpType.Pumpkin:
                    pumpkin.SetActive(true);
                    break;
            }

            return true;
        }

        return false; ;
    }

    public bool DropItem()
    {
        if (CurrentItem != PickUpType.None)
        {
            switch (currentItem)
            {
                case PickUpType.Pumpkin:
                    currentItem = PickUpType.None;
                    animator.SetBool("HasItem", false);
                    pumpkin.SetActive(false);
                    Instantiate(pumpkinPrefab, pumpkin.transform.position, Quaternion.identity);
                    break;
            }

            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactPoint.position, interactRadius);
    }
}
